using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicationsDMS.Entities.Models;

namespace PublicationsDMS.Entities.Interfaces.Repositories
{
    public interface IFolderRepository : IDisposable
    {
        Folder GetByID(int folderID);

        Folder GetParent(int folderID);
        
        IEnumerable<Folder> GetByParentID(int? parentID);

        void AddFolder(Folder folder);

        void UpdateFolder(Folder folder);

        string GetFolderName(int folderID);
    }
}
