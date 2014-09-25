using PublicationsDMS.Entities.Interfaces.Repositories;
using PublicationsDMS.Entities.Interfaces.Services;
using PublicationsDMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicationsDMS.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddUser(User user, string password)
        {
            if (!_userRepository.ExistUserWithEmail(user.Email))
            {
                _userRepository.AddUser(user, password);
            }
        }

        public void UpdateUser(User user)
        {
            _userRepository.UpdateUser(user);
        }

        public void ChangePassword(int userID, string newPassword)
        {
            _userRepository.ChangePassword(userID, newPassword);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetByID(int userID)
        {
            return _userRepository.GetByID(userID);
        }

        public User Login(string email, string password)
        {
            return _userRepository.Login(email, password);
        }

        public User GetByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }
    }
}
