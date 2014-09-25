using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicationsDMS.Services.Registration;
using PublicationsDMS.Entities.Interfaces.Repositories;
using PublicationsDMS.Entities.Interfaces.Services;
using PublicationsDMS.Entities.Models;

namespace PublicationsDMS.Services
{
    public class FolderService : IFolderService
    {
        private readonly IFolderRepository _folderRepository;
        private readonly IStorageService _storageService;

        public FolderService(IFolderRepository folderRepository, IStorageService storageService)
        {
            _folderRepository = folderRepository;
            _storageService = storageService;
        }

        public Folder GetByID(int folderID)
        {
            return _folderRepository.GetByID(folderID);
        }

        public List<Folder> GetParents(int parentID)
        {
            List<Folder> result = new List<Folder>();

            Folder folder = _folderRepository.GetByID(parentID);
            result.Add(folder);

            while (folder.ParentFolderID.HasValue)
            {
                folder = _folderRepository.GetByID(folder.ParentFolderID.Value);
                result.Add(folder);
            }

            result.Reverse();
            
            return result;
        }

        public void AddFolder(Folder folder)
        {
            _folderRepository.AddFolder(folder);
            
            CreateFolderPath(folder);
        }

        public void UpdateFolder(Folder folder)
        {
            string oldFolderName = _folderRepository.GetFolderName(folder.ID);

            _folderRepository.UpdateFolder(folder);

            if (oldFolderName != folder.Title)
            {
                string parentFolderPath = GenerateFolderPath(folder.ParentFolderID);

                _storageService.UpdateFolderPath(parentFolderPath, oldFolderName, folder.Title);
            }
        }

        public string GenerateFolderPath(int? folderID)
        {
            Folder folder = folderID.HasValue ? GetByID(folderID.Value) : null;
            return GenerateFolderPath(folder);
        }

        public string GenerateFolderPath(Folder folder)
        {
            List<Folder> folders = new List<Folder>();

            while (folder != null)
            {
                folders.Add(folder);
                folder = folder.ParentFolderID.HasValue ? GetByID(folder.ParentFolderID.Value) : null;
            }

            return _storageService.GenerateFolderPath(folders);
        }

        private string CreateFolderPath(Folder folder)
        {
            List<string> parentFolders = new List<string>();

            while (folder != null)
            {
                parentFolders.Add(folder.Title);
                folder = folder.ParentFolderID.HasValue ? GetByID(folder.ParentFolderID.Value) : null;
            }

            return _storageService.CreateFolderPath(parentFolders);
        }
    }
}
