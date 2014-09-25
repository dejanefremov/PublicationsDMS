using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicationsDMS.Entities.Interfaces.Services
{
    public interface IPermissionService
    {
        bool HasNodePermission(int nodeID, int userID);

        List<int> HasNodesPermission(List<int> nodeIDs, int userID);
    }
}
