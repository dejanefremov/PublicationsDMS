using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PublicationsDMS.Entities.Interfaces.Repositories;
using PublicationsDMS.Entities.Models;
using DomainDocument = PublicationsDMS.Domain.Models.Document;
using PublicationsDMS.Domain.Cache;
using PublicationsDMS.Entities.Enumerations;

namespace PublicationsDMS.Domain.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        #region Members

        private Domain.Models.PublicationsDMSContext _context;

        #endregion

        #region Constructor

        public DocumentRepository() : this(new Domain.Models.PublicationsDMSContext())
        {
        }

        public DocumentRepository(Domain.Models.PublicationsDMSContext context)
        {
            _context = context;
        }

        #endregion

        public IEnumerable<Document> GetByParentID(int? parentID)
        {
            IEnumerable<Document> result;

            Dictionary<int?, IEnumerable<Document>> documentsByParent = CacheController.GetFromCache(CacheController.CacheItemKey.DocumentsByParent) as Dictionary<int?, IEnumerable<Document>>;

            if (documentsByParent == null)
            {
                documentsByParent = new Dictionary<int?, IEnumerable<Document>>();
                CacheController.AddToCache(CacheController.CacheItemKey.DocumentsByParent, documentsByParent);
            }

            if (documentsByParent.ContainsKey(parentID ?? 0))
            {
                result = documentsByParent[parentID ?? 0];
            }
            else
            {
                result = Mapper.Map<IEnumerable<Document>>(_context.Documents.Where(d => d.DataItem.ParentFolderID == parentID));

                documentsByParent.Add(parentID ?? 0, result);
            }

            return result;
        }

        public Document GetByID(int documentID)
        {
            DomainDocument domainDocument = GetDomainDocumentByID(documentID);
            return Mapper.Map<Document>(domainDocument);
        }

        public IEnumerable<Document> GetByFileIDList(IEnumerable<Guid> documentFileIDs)
        {
            return Mapper.Map<IEnumerable<Document>>(_context.Documents.Where(d => documentFileIDs.Contains(d.FileID)));
        }

        public IEnumerable<Document> GetByFileIDListForParentID(IEnumerable<Guid> documentFileIDs, int parentID)
        {
            var documentsByFileIDs = _context.Documents.Where(d => documentFileIDs.Contains(d.FileID));
            List<DomainDocument> result = new List<DomainDocument>();

            List<int> childrenFolders = new List<int>();

            foreach (var document in documentsByFileIDs)
            {
                if (document.DataItem.ParentFolderID.HasValue)
                {
                    if (childrenFolders.Contains(document.DataItem.ParentFolderID.Value))
                    {
                        result.Add(document);
                    }
                    else
                    {
                        int? documentParentID = document.DataItem.ParentFolderID;


                        while (documentParentID.HasValue || documentParentID != parentID)
                        {
                            documentParentID = document.DataItem.ParentFolderID;
                        }

                        if (documentParentID.HasValue && documentParentID.Value == parentID)
                        {
                            result.Add(document);
                            childrenFolders.Add(document.DataItem.ParentFolderID.Value);
                        }
                    }
                }
            }

            return Mapper.Map<IEnumerable<Document>>(result);
        }

        private DomainDocument GetDomainDocumentByID(int documentID)
        {
            return _context.Documents.SingleOrDefault(d => d.DocumentID == documentID);
        }

        public void Save(Document document)
        {
            if (document != null)
            {
                DomainDocument domainDocument = GetDomainDocumentByID(document.ID);

                if (domainDocument != null)
                {
                    domainDocument.DataItem.Title = document.Title;
                    domainDocument.DataItem.ParentFolderID = document.ParentFolderID;
                    domainDocument.FileID = document.FileID;
                    domainDocument.FileExtension = document.FileExtension;

                    _context.Entry(domainDocument).State = System.Data.Entity.EntityState.Modified;
                    _context.Entry(domainDocument.DataItem).State = System.Data.Entity.EntityState.Modified;

                    _context.SaveChanges();

                    CacheController.ResetDataCacheItems();
                }
            }
        }

        public void UpdateDocument(Document document)
        {
            if (document != null)
            {
                DomainDocument domainDocument = GetDomainDocumentByID(document.ID);

                if (domainDocument != null)
                {
                    domainDocument.DataItem.Title = document.Title;
                    domainDocument.Description = document.Description;

                    _context.Entry(domainDocument).State = System.Data.Entity.EntityState.Modified;
                    _context.Entry(domainDocument.DataItem).State = System.Data.Entity.EntityState.Modified;

                    _context.SaveChanges();

                    CacheController.ResetDataCacheItems();
                }
            }
        }

        public void AddNewDocuments(List<Document> documents)
        {
            if (documents != null)
            {
                documents.ForEach(document =>
                    {
                        DomainDocument domainDocument = new DomainDocument { 
                            DataItem = new Models.DataItem()
                        };

                        domainDocument.DataItem.Title = document.Title;
                        domainDocument.DataItem.ParentFolderID = document.ParentFolderID;
                        domainDocument.DataItem.Type = (byte)DataItemType.Document;
                        domainDocument.FileID = document.FileID;

                        _context.Documents.Add(domainDocument);
                    });

                _context.SaveChanges();

                CacheController.ResetDataCacheItems();
            }
        }

        #region Dispose

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
