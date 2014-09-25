using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublicationsDMS.Domain.Repositories;
using DomainFolder = PublicationsDMS.Domain.Models.Folder;
using PublicationsDMS.Entities.Interfaces.Repositories;
using Autofac;

namespace PublicationsDMS.Test
{
    [TestClass]
    public class FolderRepositoryTests
    {
        private readonly IFolderRepository _folderRepository;

        public FolderRepositoryTests()
        {
            _folderRepository = new FolderRepository();
        }
    }
}
