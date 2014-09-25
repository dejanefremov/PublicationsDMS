using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicationsDMS.Entities.Models;

namespace PublicationsDMS.Entities.Interfaces.Repositories
{
    public interface IDataItemRepository : IDisposable
    {
        DataItem GetByID(int dataItemID);

        IEnumerable<DataItem> GetAllByParentID(int? parentID);

        IEnumerable<DataItem> GetByParentID(int parentID, int userID);

        IEnumerable<DataItem> GetByUserID(int userID);
    }
}
