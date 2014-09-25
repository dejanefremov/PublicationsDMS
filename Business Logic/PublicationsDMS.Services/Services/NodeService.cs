using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicationsDMS.Entities.Interfaces.Models;
using PublicationsDMS.Entities.Interfaces.Repositories;
using PublicationsDMS.Entities.Interfaces.Services;
using PublicationsDMS.Entities.Models;

namespace PublicationsDMS.Services
{
    public class NodeService : INodeService
    {
        private readonly IFolderRepository _folderRepository;
        private readonly IDataItemRepository _dataItemRepository;
        private readonly IPermissionService _permissionService;
        private readonly IUserDataItemRepository _userDataItemRepository;

        public NodeService(IFolderRepository folderRepository, 
            IDataItemRepository dataItemRepository,
            IPermissionService permissionService,
            IUserDataItemRepository userDataItemRepository)
        {
            _folderRepository = folderRepository;
            _dataItemRepository = dataItemRepository;
            _permissionService = permissionService;
            _userDataItemRepository = userDataItemRepository;
        }

        public IEnumerable<INode> GetByParentID(int? parentID, int userID)
        {
            IEnumerable<INode> result = new List<INode>();

            if (parentID.HasValue)
            {
                if (_permissionService.HasNodePermission(parentID.Value, userID))
                {
                    result = _dataItemRepository.GetAllByParentID(parentID);
                }
            }
            else
            {
                result = _dataItemRepository.GetByUserID(userID);
            }

            return result;
        }

        public IEnumerable<INode> GetAllByParentID(int? parentID)
        {
            return _dataItemRepository.GetAllByParentID(parentID);
        }

        public INode GetByID(int nodeID, int userID)
        {
            if (_permissionService.HasNodePermission(nodeID, userID))
            {
                return _dataItemRepository.GetByID(nodeID);
            }

            return null;
        }


        public INode GetParentFolder(int nodeID, int userID)
        {
            var parentFolder = _folderRepository.GetParent(nodeID);

            if (parentFolder != null && !_permissionService.HasNodePermission(parentFolder.ID, userID))
            {
                return null;
            }

            return parentFolder;
        }

        public void SaveDataItemUsers(int dataItemID, List<int> userIDs)
        {
            userIDs = userIDs.Distinct().ToList();

            var dataItemUsers = _userDataItemRepository.GetDataItemUsers(dataItemID);

            foreach (var dataItemUser in dataItemUsers)
            {
                if (!userIDs.Contains(dataItemUser.UserID))
                {
                    _userDataItemRepository.RemoveDataItemPermission(dataItemID, dataItemUser.UserID);
                }
            }

            var permissionUsersIDs = dataItemUsers.Select(user => user.UserID);
            foreach (int userID in userIDs)
            {
                if (!permissionUsersIDs.Contains(userID))
                {
                    _userDataItemRepository.AddDataItemPermission(dataItemID, userID);
                }
            }

            _userDataItemRepository.SaveChanges();
        }

        public IEnumerable<User> GetNodeUsers(int nodeID)
        {
            return _userDataItemRepository.GetDataItemUsers(nodeID);
        }

        public List<DataItem> GetParents(int nodeID, int userID)
        {
            List<DataItem> result = new List<DataItem>();

            int? nID = nodeID;
            bool hasPermission = false;

            while (nID.HasValue)
            {
                DataItem folder = _dataItemRepository.GetByID(nID.Value);
                result.Add(folder);

                if (_userDataItemRepository.HasPermission(nID.Value, userID))
                {
                    nID = null;
                    hasPermission = true;
                }
                else
                {
                    nID = folder.ParentFolderID;
                }
            }

            if (!hasPermission)
            {
                return new List<DataItem>();
            }

            result.Reverse();
            return result;
        }

        public List<DataItem> GetAllParents(int parentID)
        {
            List<DataItem> result = new List<DataItem>();

            DataItem folder = _dataItemRepository.GetByID(parentID);
            if (folder != null)
            {
                result.Add(folder);

                while (folder.ParentFolderID.HasValue)
                {
                    folder = _dataItemRepository.GetByID(folder.ParentFolderID.Value);
                    result.Add(folder);
                }

                result.Reverse();
            }

            return result;
        }
    }
}
