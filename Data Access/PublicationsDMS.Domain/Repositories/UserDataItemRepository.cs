using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDataItem = PublicationsDMS.Domain.Models.DataItem;
using DomainUser = PublicationsDMS.Domain.Models.User;
using PublicationsDMS.Entities.Interfaces.Repositories;
using PublicationsDMS.Domain.Cache;
using PublicationsDMS.Entities.Models;
using AutoMapper;

namespace PublicationsDMS.Domain.Repositories
{
    public class UserDataItemRepository : IUserDataItemRepository
    {
        #region Members

        private Domain.Models.PublicationsDMSContext _context;

        #endregion

        #region Constructor

        public UserDataItemRepository() : this(new Domain.Models.PublicationsDMSContext())
        {
        }

        public UserDataItemRepository(Domain.Models.PublicationsDMSContext context)
        {
            _context = context;
        }

        #endregion

        public IEnumerable<User> GetDataItemUsers(int dataItemID)
        {
            IEnumerable<User> users = new List<User>();

            DomainDataItem domainDataItem = _context.DataItems.SingleOrDefault(f => f.DataItemID == dataItemID);

            if(domainDataItem != null)
            {
                users = Mapper.Map<IEnumerable<User>>(domainDataItem.UserDataItemPermissions.Select(perm =>perm.User));
            }

            return users;
        }

        public IEnumerable<int> GetUserDataItemIDs(int userID)
        {
            return _context.UserDataItemPermissions.Where(udp => udp.UserID == userID).Select(perm => perm.DataItemID);
        }

        public void RemoveDataItemPermission(int dataItemID, int userID)
        {
            var permission = _context.UserDataItemPermissions.SingleOrDefault(perm => perm.DataItemID == dataItemID && perm.UserID == userID);
            if (permission != null)
            {
                _context.UserDataItemPermissions.Remove(permission);
            }
        }

        public void AddDataItemPermission(int dataItemID, int userID)
        {
            _context.UserDataItemPermissions.Add(new Models.UserDataItemPermission
            {
                DataItemID = dataItemID,
                UserID = userID
            });
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool HasPermission(int dataItemID, int userID)
        {
            return _context.UserDataItemPermissions.Any(f => f.DataItemID == dataItemID && f.UserID == userID);
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
