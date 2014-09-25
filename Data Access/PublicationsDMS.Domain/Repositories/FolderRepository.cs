using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainFolder = PublicationsDMS.Domain.Models.Folder;
using PublicationsDMS.Entities.Interfaces.Repositories;
using PublicationsDMS.Domain.Cache;
using PublicationsDMS.Entities.Models;
using AutoMapper;
using PublicationsDMS.Entities.Enumerations;

namespace PublicationsDMS.Domain.Repositories
{
    public class FolderRepository : IFolderRepository
    {
        #region Members

        private Domain.Models.PublicationsDMSContext _context;

        #endregion

        #region Constructor

        public FolderRepository() : this(new Domain.Models.PublicationsDMSContext())
        {
        }

        public FolderRepository(Domain.Models.PublicationsDMSContext context)
        {
            _context = context;
        }

        #endregion

        public IEnumerable<Folder> GetByParentID(int? parentID)
        {
            IEnumerable<Folder> result;

            Dictionary<int?, IEnumerable<Folder>> foldersByParent = CacheController.GetFromCache(CacheController.CacheItemKey.FoldersByParent) as Dictionary<int?, IEnumerable<Folder>>;

            if (foldersByParent == null)
            {
                foldersByParent = new Dictionary<int?, IEnumerable<Folder>>();
                CacheController.AddToCache(CacheController.CacheItemKey.FoldersByParent, foldersByParent);
            }

            if (foldersByParent.ContainsKey(parentID ?? 0))
            {
                result = foldersByParent[parentID ?? 0];
            }
            else
            {
                result = Mapper.Map<IEnumerable<Folder>>(_context.Folders.Include("DataItem").Where(f => f.DataItem.ParentFolderID == parentID));

                foldersByParent.Add(parentID ?? 0, result);
            }

            return result;
        }

        public Folder GetByID(int folderID)
        {
            DomainFolder domainFolder = GetDomainFolderByID(folderID);
            return Mapper.Map<Folder>(domainFolder);
        }

        private DomainFolder GetDomainFolderByID(int folderID)
        {
            return _context.Folders.Include("DataItem").SingleOrDefault(f => f.FolderID == folderID);
        }

        public Folder GetParent(int folderID)
        {
            Folder folder = GetByID(folderID);

            if (folder != null && folder.ParentFolderID.HasValue)
            {
                return GetByID(folder.ParentFolderID.Value);
            }
            return null;
        }

        public void AddFolder(Folder folder)
        {
            if (folder != null)
            {
                DomainFolder domainFolder = new DomainFolder { 
                    DataItem = new Models.DataItem()
                };

                domainFolder.DataItem.Title = folder.Title;
                domainFolder.DataItem.ParentFolderID = folder.ParentFolderID;
                domainFolder.DataItem.Type = (byte)DataItemType.Folder;

                _context.Folders.Add(domainFolder);

                _context.SaveChanges();

                CacheController.ResetDataCacheItems();
            }
        }

        public void UpdateFolder(Folder folder)
        {
            if (folder != null)
            {
                DomainFolder domainFolder = GetDomainFolderByID(folder.ID);

                if (domainFolder != null)
                {
                    domainFolder.DataItem.Title = folder.Title;
                    domainFolder.DataItem.ParentFolderID = folder.ParentFolderID;

                    _context.Entry(domainFolder).State = System.Data.Entity.EntityState.Modified;
                    _context.Entry(domainFolder.DataItem).State = System.Data.Entity.EntityState.Modified;

                    _context.SaveChanges();

                    CacheController.ResetDataCacheItems();
                }
            }
        }

        public string GetFolderName(int folderID)
        {
            return (from folder in _context.Folders
                    where folder.FolderID == folderID
                    select folder.DataItem.Title).SingleOrDefault();
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
