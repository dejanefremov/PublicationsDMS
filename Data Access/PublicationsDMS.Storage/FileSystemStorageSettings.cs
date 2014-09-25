using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicationsDMS.Storage
{
    public class FileSystemStorageSettings
    {
        public static void Initialize(string applicationRootFolder)
        {
            RootFolder = applicationRootFolder;
        }

        internal static string RootFolder
        {
            get;
            private set;
        }

        internal static string TempDocumentsFolder
        {
            get
            {
                return string.Format("{0}\\TempDocuments", RootFolder);
            }
        }

        internal static string DocumentsFolder
        {
            get
            {
                return string.Format("{0}\\Documents", RootFolder);
            }
        }

        internal static string LuceneIndexFolder
        {
            get
            {
                return string.Format("{0}\\LuceneIndex", RootFolder);
            }
        }
    }
}
