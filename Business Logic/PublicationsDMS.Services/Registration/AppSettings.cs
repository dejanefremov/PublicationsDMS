using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicationsDMS.Services.Registration
{
    public class AppSettings
    {
        public static void Initialize(string applicationRootFolder)
        {
            PublicationsDMS.Search.SearchSettings.Initialize(applicationRootFolder);
            PublicationsDMS.Storage.FileSystemStorageSettings.Initialize(applicationRootFolder);
        }
    }
}
