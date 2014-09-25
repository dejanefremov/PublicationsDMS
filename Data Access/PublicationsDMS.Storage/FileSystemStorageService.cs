using PublicationsDMS.Entities.Interfaces.Repositories;
using PublicationsDMS.Entities.Interfaces.Services;
using PublicationsDMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicationsDMS.Storage
{
    public class FileSystemStorageService : IStorageService
    {
        public FileSystemStorageService()
        {
        }

        public void SaveDocument(string folderPath, Guid tempDocumentFileID, Guid fileID)
        {
            string tempFilePath = string.Format("{0}\\{1}", FileSystemStorageSettings.TempDocumentsFolder, tempDocumentFileID);
            string destinationFilePath = string.Format("{0}\\{1}", folderPath, fileID);

            File.Move(tempFilePath, destinationFilePath);
        }

        public Guid SaveTempDocument(Stream uploadedStream)
        {
            try
            {
                Guid fileID = Guid.NewGuid();

                using (var fileStream = File.Create(string.Format("{0}\\{1}", FileSystemStorageSettings.TempDocumentsFolder, fileID)))
                {
                    uploadedStream.CopyTo(fileStream);
                }

                return fileID;
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public string GenerateFolderPath(List<Folder> folders)
        {
            string filePath = string.Empty;

            folders.ForEach(folder =>
            {
                filePath = string.Format("{0}\\{1}", folder.Title, filePath);
            });

            return string.Format("{0}\\{1}", FileSystemStorageSettings.DocumentsFolder, filePath);
        }

        public string CreateFolderPath(List<string> parentFolders)
        {
            string currentFolderPath = FileSystemStorageSettings.DocumentsFolder;
            parentFolders.Reverse();
            parentFolders.ForEach(pf =>
            {
                currentFolderPath = string.Format("{0}\\{1}", currentFolderPath, pf);
                if (!Directory.Exists(currentFolderPath))
                {
                    Directory.CreateDirectory(currentFolderPath);
                }
            });

            return currentFolderPath;
        }

        public void UpdateFolderPath(string parentFolderPath, string oldFolderName, string newFolderName)
        {
            string oldFolderPath = string.Format("{0}\\{1}", parentFolderPath, oldFolderName);
            string newFolderPath = string.Format("{0}\\{1}", parentFolderPath, newFolderName);

            Directory.Move(oldFolderPath, newFolderPath);
        }
    }
}
