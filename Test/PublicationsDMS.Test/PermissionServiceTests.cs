using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublicationsDMS.Domain.Repositories;
using DomainFolder = PublicationsDMS.Domain.Models.Folder;
using PublicationsDMS.Entities.Interfaces.Repositories;
using Autofac;
using PublicationsDMS.Entities.Interfaces.Services;
using PublicationsDMS.Services;
using PublicationsDMS.Entities.Models;
using System.Collections.Generic;

namespace PublicationsDMS.Test
{
    [TestClass]
    public class PermissionServiceTests
    {
        private readonly IPermissionService _permissionService;

        public PermissionServiceTests()
        {
            _permissionService = new PermissionService(
                new TestDataItemRepository(),
                new TestUserRepository(),
                new TestUserDataItemRepository());
        }

        [TestMethod]
        public void TestHasNodesPermission1()
        {
            List<int> nodesToCheck = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            List<int> expected = new List<int> { 3, 5, 6, 7, 8, 9, 10, 11, 12 };

            List<int> actual = _permissionService.HasNodesPermission(nodesToCheck, 1);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestHasNodesPermission2()
        {
            List<int> nodesToCheck = new List<int> { 8, 9, 10, 11, 12 };
            List<int> expected = new List<int> { 8, 9, 10, 11, 12 };

            List<int> actual = _permissionService.HasNodesPermission(nodesToCheck, 1);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestHasNodesPermission3()
        {
            List<int> nodesToCheck = new List<int> { 1, 2, 4, 5, 7, 8, 12 };
            List<int> expected = new List<int> { 5, 7, 8, 12 };

            List<int> actual = _permissionService.HasNodesPermission(nodesToCheck, 1);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestHasNodesPermission4()
        {
            List<int> nodesToCheck = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            List<int> expected = new List<int> { 2, 3, 5, 6, 7, 8, 9, 10, 11, 12 };

            List<int> actual = _permissionService.HasNodesPermission(nodesToCheck, 2);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestHasNodePermission()
        {
            bool actual = _permissionService.HasNodePermission(8, 1);

            Assert.IsTrue(actual);
        }
    }

    #region Dummy Repositories

    internal class TestUserRepository : IUserRepository
    {
        private List<User> users = new List<User>
        {
            new User{ UserID = 1, Name = "Test User 1", IsAdministrator = false, IsActive = true, Email = "TestUser1@test.com" },
            new User{ UserID = 2, Name = "Test User 2", IsAdministrator = false, IsActive = true, Email = "TestUser2@test.com" },
            new User{ UserID = 3, Name = "Test Admin 1", IsAdministrator = true, IsActive = true, Email = "TestAdmin1@test.com" },
            new User{ UserID = 4, Name = "Test Admin 2", IsAdministrator = true, IsActive = true, Email = "TestAdmin2@test.com" },
        };

        public PublicationsDMS.Entities.Models.User GetByID(int userID)
        {
            return users.Find(u => u.UserID == userID);
        }

        public void AddUser(PublicationsDMS.Entities.Models.User user, string password)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(PublicationsDMS.Entities.Models.User user)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<PublicationsDMS.Entities.Models.User> GetAll()
        {
            throw new NotImplementedException();
        }

        public PublicationsDMS.Entities.Models.User Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void ChangePassword(int userID, string newPassword)
        {
            throw new NotImplementedException();
        }

        public bool ExistUserWithEmail(string email)
        {
            throw new NotImplementedException();
        }

        public PublicationsDMS.Entities.Models.User GetByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }

    internal class TestDataItemRepository : IDataItemRepository
    {
        List<DataItem> dataItems = new List<DataItem>
        {
            new DataItem { ID = 1, ParentFolderID = null, Title = "Level 1 - 1", Type = PublicationsDMS.Entities.Enumerations.DataItemType.Folder },
            new DataItem { ID = 2, ParentFolderID = null, Title = "Level 1 - 2", Type = PublicationsDMS.Entities.Enumerations.DataItemType.Document },

            new DataItem { ID = 3, ParentFolderID = 1, Title = "Level 2 - 1", Type = PublicationsDMS.Entities.Enumerations.DataItemType.Folder },
            new DataItem { ID = 4, ParentFolderID = 1, Title = "Level 2 - 2", Type = PublicationsDMS.Entities.Enumerations.DataItemType.Document },
            new DataItem { ID = 5, ParentFolderID = 1, Title = "Level 2 - 3", Type = PublicationsDMS.Entities.Enumerations.DataItemType.Document },

            new DataItem { ID = 6, ParentFolderID = 3, Title = "Level 3 - 1", Type = PublicationsDMS.Entities.Enumerations.DataItemType.Folder },
            new DataItem { ID = 7, ParentFolderID = 3, Title = "Level 3 - 2", Type = PublicationsDMS.Entities.Enumerations.DataItemType.Document },
            new DataItem { ID = 8, ParentFolderID = 3, Title = "Level 3 - 3", Type = PublicationsDMS.Entities.Enumerations.DataItemType.Document },
            new DataItem { ID = 9, ParentFolderID = 3, Title = "Level 3 - 4", Type = PublicationsDMS.Entities.Enumerations.DataItemType.Document },

            new DataItem { ID = 10, ParentFolderID = 6, Title = "Level 4 - 1", Type = PublicationsDMS.Entities.Enumerations.DataItemType.Document },
            new DataItem { ID = 11, ParentFolderID = 6, Title = "Level 4 - 2", Type = PublicationsDMS.Entities.Enumerations.DataItemType.Document },
            new DataItem { ID = 12, ParentFolderID = 6, Title = "Level 4 - 3", Type = PublicationsDMS.Entities.Enumerations.DataItemType.Document },
        };

        public DataItem GetByID(int dataItemID)
        {
            return dataItems.Find(di => di.ID == dataItemID);
        }

        public IEnumerable<DataItem> GetAllByParentID(int? parentID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DataItem> GetByParentID(int parentID, int userID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DataItem> GetByUserID(int userID)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }


    internal class TestUserDataItemRepository : IUserDataItemRepository
    {
        List<PublicationsDMS.Domain.Models.UserDataItemPermission> permissions = new List<PublicationsDMS.Domain.Models.UserDataItemPermission>
        {
            new PublicationsDMS.Domain.Models.UserDataItemPermission { UserDataItemPermissionID = 1, DataItemID = 3, UserID = 1 },
            new PublicationsDMS.Domain.Models.UserDataItemPermission { UserDataItemPermissionID = 2, DataItemID = 5, UserID = 1 },

            new PublicationsDMS.Domain.Models.UserDataItemPermission { UserDataItemPermissionID = 3, DataItemID = 2, UserID = 2 },
            new PublicationsDMS.Domain.Models.UserDataItemPermission { UserDataItemPermissionID = 4, DataItemID = 3, UserID = 2 },
            new PublicationsDMS.Domain.Models.UserDataItemPermission { UserDataItemPermissionID = 5, DataItemID = 5, UserID = 2 },
        };

        public IEnumerable<User> GetDataItemUsers(int dataItemID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> GetUserDataItemIDs(int userID)
        {
            throw new NotImplementedException();
        }

        public void RemoveDataItemPermission(int dataItemID, int userID)
        {
            throw new NotImplementedException();
        }

        public void AddDataItemPermission(int dataItemID, int userID)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public bool HasPermission(int dataItemID, int userID)
        {
            return permissions.Exists(p => p.DataItemID == dataItemID && p.UserID == userID);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }



    #endregion
}
