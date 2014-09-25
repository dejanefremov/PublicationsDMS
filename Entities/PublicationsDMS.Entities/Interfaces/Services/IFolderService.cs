using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicationsDMS.Entities.Models;

namespace PublicationsDMS.Entities.Interfaces.Services
{
    public interface IFolderService
    {
        Folder GetByID(int folderID);

        void AddFolder(Folder folder);

        void UpdateFolder(Folder folder);

        string GenerateFolderPath(int? folderID);

        string GenerateFolderPath(Folder folder);

        List<Folder> GetParents(int parentID);
        
    }
}
