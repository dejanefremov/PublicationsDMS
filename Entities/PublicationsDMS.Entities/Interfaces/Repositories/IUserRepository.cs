using PublicationsDMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicationsDMS.Entities.Interfaces.Repositories
{
    public interface IUserRepository
    {
        User GetByID(int userID);

        void AddUser(User user, string password);

        void UpdateUser(User user);

        IEnumerable<User> GetAll();

        User Login(string email, string password);

        void ChangePassword(int userID, string newPassword);

        bool ExistUserWithEmail(string email);

        User GetByEmail(string email);
    }
}
