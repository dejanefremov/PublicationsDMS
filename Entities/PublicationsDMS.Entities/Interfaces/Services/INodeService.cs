using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicationsDMS.Entities.Interfaces.Models;
using PublicationsDMS.Entities.Models;

namespace PublicationsDMS.Entities.Interfaces.Services
{
    public interface INodeService
    {
        IEnumerable<INode> GetAllByParentID(int? parentID);

        IEnumerable<INode> GetByParentID(int? parentID, int userID);

        INode GetByID(int nodeID, int userID);

        INode GetParentFolder(int nodeID, int userID);

        IEnumerable<User> GetNodeUsers(int nodeID);

        void SaveDataItemUsers(int dataItemID, List<int> userIDs);

        List<DataItem> GetParents(int nodeID, int userID);

        List<DataItem> GetAllParents(int parentID);
    }
}
