using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainUser = PublicationsDMS.Domain.Models.User;
using PublicationsDMS.Entities.Interfaces.Repositories;
using PublicationsDMS.Domain.Cache;
using PublicationsDMS.Entities.Models;
using AutoMapper;
using PublicationsDMS.Domain.Security;

namespace PublicationsDMS.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Members

        private Domain.Models.PublicationsDMSContext _context;

        #endregion

        #region Constructor

        public UserRepository() : this(new Domain.Models.PublicationsDMSContext())
        {
        }

        public UserRepository(Domain.Models.PublicationsDMSContext context)
        {
            _context = context;
        }

        #endregion

        public User GetByID(int userID)
        {
            DomainUser domainUser = GetDomainUserByID(userID);
            return Mapper.Map<User>(domainUser);
        }

        public User GetByEmail(string email)
        {
            DomainUser domainUser = GetDomainUserByEmail(email);
            return Mapper.Map<User>(domainUser);
        }

        public IEnumerable<User> GetAll()
        {
            return Mapper.Map<IEnumerable<User>>(_context.Users);
        }

        private DomainUser GetDomainUserByID(int userID)
        {
            return _context.Users.SingleOrDefault(f => f.UserID == userID);
        }

        private DomainUser GetDomainUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(f => f.Email == email);
        }

        public void AddUser(User user, string password)
        {
            if (user != null && !string.IsNullOrEmpty(password))
            {
                DomainUser domainUser = new DomainUser();

                domainUser.Name = user.Name;
                domainUser.Email = user.Email;
                domainUser.IsAdministrator = user.IsAdministrator;
                domainUser.IsActive = user.IsActive;

                byte[] salt, hashedPassword;
                PasswordEncoder.EncryptPassword(password, out salt, out hashedPassword);
                domainUser.Password = hashedPassword;
                domainUser.Salt = salt;

                _context.Users.Add(domainUser);

                _context.SaveChanges();

                CacheController.ResetCacheItem(CacheController.CacheItemKey.UserByID);
                CacheController.ResetCacheItem(CacheController.CacheItemKey.UserByEmail);
            }
        }

        public bool ExistUserWithEmail(string email)
        {
            return _context.Users.Any(u => u.Email.Equals(email.Trim()));
        }

        public void ChangePassword(int userID, string newPassword)
        {
            DomainUser domainUser = GetDomainUserByID(userID);

            if (domainUser != null && !string.IsNullOrEmpty(newPassword))
            {
                byte[] salt, hashedPassword;
                PasswordEncoder.EncryptPassword(newPassword, out salt, out hashedPassword);
                domainUser.Password = hashedPassword;
                domainUser.Salt = salt;

                _context.Entry(domainUser).State = System.Data.Entity.EntityState.Modified;

                _context.SaveChanges();

                CacheController.ResetCacheItem(CacheController.CacheItemKey.UserByID);
                CacheController.ResetCacheItem(CacheController.CacheItemKey.UserByEmail);
            }
        }

        public User Login(string email, string password)
        {
            DomainUser domainUser = GetDomainUserByEmail(email);

            if (domainUser != null && PasswordEncoder.CheckPassword(password, domainUser.Salt, domainUser.Password))
            {
                return Mapper.Map<User>(domainUser);
            }

            return null;
        }

        public void UpdateUser(User user)
        {
            if (user != null)
            {
                DomainUser domainUser = GetDomainUserByID(user.UserID);

                if (domainUser != null)
                {
                    domainUser.Name = user.Name;

                    _context.Entry(domainUser).State = System.Data.Entity.EntityState.Modified;

                    _context.SaveChanges();

                    CacheController.ResetCacheItem(CacheController.CacheItemKey.UserByID);
                    CacheController.ResetCacheItem(CacheController.CacheItemKey.UserByEmail);
                }
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
