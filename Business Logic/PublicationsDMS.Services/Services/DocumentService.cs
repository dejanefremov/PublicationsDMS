using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicationsDMS.Services.Registration;
using PublicationsDMS.Entities.Interfaces.Repositories;
using PublicationsDMS.Entities.Interfaces.Services;
using PublicationsDMS.Entities.Models;

namespace PublicationsDMS.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IFolderService _folderService;
        private readonly IPermissionService _permissionService;
        private readonly ISearchService _searchService;
        private readonly IStorageService _storageService;

        public DocumentService(IDocumentRepository documentRepository
            , IFolderService folderService
            , IPermissionService permissionService
            , ISearchService searchService
            , IStorageService storageService)
        {
            _documentRepository = documentRepository;
            _folderService = folderService;
            _permissionService = permissionService;
            _searchService = searchService;
            _storageService = storageService;
        }

        public Document GetByID(int documentID)
        {
            return _documentRepository.GetByID(documentID);
        }

        public void Save(Document document)
        {
            _documentRepository.Save(document);
        }

        public void UpdateDocument(Document document)
        {
            _documentRepository.UpdateDocument(document);
        }

        public void AddNewDocuments(int? parentFolderID, List<Document> documents)
        {
            try
            {
                foreach (Document document in documents)
                {
                    string folderPath = _folderService.GenerateFolderPath(parentFolderID);
                    Guid tempDocumentFileID = document.FileID;
                    Guid fileID = Guid.NewGuid();

                    document.FileID = fileID;
                    document.FileExtension = Path.GetExtension(document.Title);

                    _storageService.SaveDocument(folderPath, tempDocumentFileID, fileID);
                }

                _documentRepository.AddNewDocuments(documents);

                _searchService.IndexDocuments(documents);
            }
            catch { }
        }

        public IEnumerable<Document> SearchDocuments(string criteria, int? parentID, int userID)
        {
            var documentFileIDs = _searchService.SearchDocuments(criteria);
            var documentsByFileIDs = _documentRepository.GetByFileIDList(documentFileIDs);

            if (!parentID.HasValue)
            {
                List<int> allowedResultIDs = _permissionService.HasNodesPermission(documentsByFileIDs.Select(r => r.ID).ToList(), userID);
                documentsByFileIDs = documentsByFileIDs.Where(r => allowedResultIDs.Contains(r.ID)); 

                return documentsByFileIDs;
            }

            List<Document> result = new List<Document>();
            List<int> childrenFolders = new List<int> { parentID.Value };

            foreach (var document in documentsByFileIDs)
            {
                if (document.ParentFolderID.HasValue)
                {
                    if (childrenFolders.Contains(document.ParentFolderID.Value))
                    {
                        result.Add(document);
                    }
                    else
                    {
                        int? parentFolderID = document.ParentFolderID;

                        while (parentFolderID.HasValue)
                        {
                            if (childrenFolders.Contains(parentFolderID.Value))
                            {
                                result.Add(document);
                                childrenFolders.Add(document.ParentFolderID.Value);

                                parentFolderID = null;
                            }
                            else
                            {
                                parentFolderID = _folderService.GetByID(parentFolderID.Value).ParentFolderID;
                            }
                        }
                    }
                }
            }

            return result;
        }

        public Guid SaveTempFile(Stream uploadedStream)
        {
            return _storageService.SaveTempDocument(uploadedStream);
        }
    }
}
