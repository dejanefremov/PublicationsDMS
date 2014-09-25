using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicationsDMS.Entities.Interfaces.Services;
using PublicationsDMS.Entities.Interfaces.Repositories;

namespace PublicationsDMS.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IDataItemRepository _dataItemRepository;
        private readonly IUserDataItemRepository _userDataItemRepository;
        private readonly IUserRepository _userRepository;

        public PermissionService(IDataItemRepository dataItemRepository,
            IUserRepository userRepository, 
            IUserDataItemRepository userDataItemRepository)
        {
            _dataItemRepository = dataItemRepository;
            _userRepository = userRepository;
            _userDataItemRepository = userDataItemRepository;
        }

        public bool HasNodePermission(int nodeID, int userID)
        {
            if (_userRepository.GetByID(userID).IsAdministrator)
            {
                return true;
            }

            bool hasPermission = false;
            var node = _dataItemRepository.GetByID(nodeID);
            hasPermission = _userDataItemRepository.HasPermission(nodeID, userID);

            while (node.ParentFolderID.HasValue && !hasPermission)
            {
                node = _dataItemRepository.GetByID(node.ParentFolderID.Value);
                hasPermission = _userDataItemRepository.HasPermission(node.ID, userID);
            }

            return hasPermission;
        }

        public List<int> HasNodesPermission(List<int> nodeIDs, int userID)
        {
            List<int> result = new List<int>();

            if (_userRepository.GetByID(userID).IsAdministrator)
            {
                return nodeIDs;
            }

            Dictionary<int, bool> nodePermissions = new Dictionary<int, bool>();

            foreach (int nodeID in nodeIDs)
            {
                List<int> parentNodes = new List<int>();
                if (nodePermissions.ContainsKey(nodeID))
                {
                    continue;
                }

                bool hasPermission = false;
                var node = _dataItemRepository.GetByID(nodeID);
                parentNodes.Add(node.ID);
                hasPermission = _userDataItemRepository.HasPermission(nodeID, userID);

                while (node.ParentFolderID.HasValue && !hasPermission)
                {
                    node = _dataItemRepository.GetByID(node.ParentFolderID.Value);

                    if (nodePermissions.ContainsKey(node.ID))
                    {
                        hasPermission = nodePermissions[node.ID];
                        break;
                    }
                    else
                    {
                        hasPermission = _userDataItemRepository.HasPermission(node.ID, userID);
                    }
                }

                if (hasPermission)
                {
                    result.Add(nodeID);
                }

                parentNodes.ForEach(pn =>
                    {
                        nodePermissions.Add(pn, hasPermission);
                    });
            }

            return result;
        }
    }
}
