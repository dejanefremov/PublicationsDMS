using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDataItem = PublicationsDMS.Domain.Models.DataItem;
using PublicationsDMS.Domain.Cache;
using PublicationsDMS.Entities.Interfaces.Repositories;
using PublicationsDMS.Entities.Models;
using AutoMapper;

namespace PublicationsDMS.Domain.Repositories
{
    public class DataItemRepository : IDataItemRepository
    {
        #region Members

        private Domain.Models.PublicationsDMSContext _context;

        #endregion

        #region Constructor

        public DataItemRepository() : this(new Domain.Models.PublicationsDMSContext())
        {
        }

        public DataItemRepository(Domain.Models.PublicationsDMSContext context)
        {
            _context = context;
        }

        #endregion

        public DataItem GetByID(int dataItemID)
        {
            DomainDataItem domainDataItem = _context.DataItems.SingleOrDefault(f => f.DataItemID == dataItemID);
            return Mapper.Map<DataItem>(domainDataItem);
        }

        public IEnumerable<DataItem> GetAllByParentID(int? parentID)
        {
            IEnumerable<DataItem> result;

            Dictionary<int?, IEnumerable<DataItem>> dataItemsByParent = CacheController.GetFromCache(CacheController.CacheItemKey.DataItemsByParent) as Dictionary<int?, IEnumerable<DataItem>>;

            if (dataItemsByParent == null)
            {
                dataItemsByParent = new Dictionary<int?, IEnumerable<DataItem>>();
                CacheController.AddToCache(CacheController.CacheItemKey.DataItemsByParent, dataItemsByParent);
            }

            if (dataItemsByParent.ContainsKey(parentID ?? 0))
            {
                result = dataItemsByParent[parentID ?? 0];
            }
            else
            {
                result = Mapper.Map<IEnumerable<DataItem>>(_context.DataItems.Where(d => d.ParentFolderID == parentID));

                dataItemsByParent.Add(parentID ?? 0, result);
            }

            return result;
        }

        public IEnumerable<DataItem> GetByParentID(int parentID, int userID)
        {
            IEnumerable<DataItem> result;

            var domainResult = GetDomainByUserID(userID)
                .Where(di => di.ParentFolderID == parentID);

            result = Mapper.Map<IEnumerable<DataItem>>(domainResult);

            return result;
        }

        public IEnumerable<DataItem> GetByUserID(int userID)
        {
            IEnumerable<DataItem> result;

            var domainResult = GetDomainByUserID(userID);

            result = Mapper.Map<IEnumerable<DataItem>>(domainResult);

            return result;
        }

        private IEnumerable<DomainDataItem> GetDomainByUserID(int userID)
        {
            return _context.UserDataItemPermissions.Where(perm => perm.UserID == userID).Join(
                _context.DataItems,
                perm => perm.DataItemID,
                di => di.DataItemID,
                (perm, di) => new { Permissions = perm, DataItems = di })
                .Select(r => r.DataItems);
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
