using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicationsDMS.Web.Api.Models
{
    public class SearchDocumentResponse
    {
        public int ID
        {
            get;
            set;
        }

        public int? ParentFolderID
        {
            get;
            set;

        }

        public string Title
        {
            get;
            set;
        }

        public IEnumerable<string> Path
        {
            get;
            set;
        }
    }
}