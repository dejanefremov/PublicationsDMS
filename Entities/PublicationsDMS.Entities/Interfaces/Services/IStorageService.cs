using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicationsDMS.Entities.Models;

namespace PublicationsDMS.Entities.Interfaces.Services
{
    public interface IStorageService
    {
        Guid SaveTempDocument(Stream uploadedStream);

        void SaveDocument(string folderPath, Guid tempDocumentFileID, Guid fileID);

        string GenerateFolderPath(List<Folder> folders);

        string CreateFolderPath(List<string> parentFolders);

        void UpdateFolderPath(string parentFolderPath, string oldFolderName, string newFolderName);
    }
}
