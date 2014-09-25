using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicationsDMS.Search
{
    public class SearchSettings
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

        internal static string LuceneIndexFolder
        {
            get
            {
                return string.Format("{0}\\LuceneIndex", RootFolder);
            }
        }
    }
}
