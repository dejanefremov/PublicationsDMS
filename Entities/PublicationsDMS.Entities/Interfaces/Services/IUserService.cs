using PublicationsDMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicationsDMS.Entities.Interfaces.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();

        void AddUser(User user, string password);

        User GetByID(int userID);

        User Login(string email, string password);

        void UpdateUser(User user);

        void ChangePassword(int userID, string newPassword);

        User GetByEmail(string email);
    }
}
