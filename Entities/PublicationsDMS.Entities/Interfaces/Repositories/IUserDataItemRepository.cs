using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicationsDMS.Entities.Models;

namespace PublicationsDMS.Entities.Interfaces.Repositories
{
    public interface IUserDataItemRepository : IDisposable
    {
        IEnumerable<User> GetDataItemUsers(int dataItemID);

        IEnumerable<int> GetUserDataItemIDs(int userID);

        void RemoveDataItemPermission(int dataItemID, int userID);

        void AddDataItemPermission(int dataItemID, int userID);

        void SaveChanges();

        bool HasPermission(int dataItemID, int userID);
    }
}
