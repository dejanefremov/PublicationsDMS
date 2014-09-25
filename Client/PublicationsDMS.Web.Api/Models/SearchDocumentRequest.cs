using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicationsDMS.Web.Api.Models
{
    public class SearchDocumentRequest
    {
        public int? ParentFolderID
        {
            get;
            set;
        }

        public string SearchingTerm
        { 
            get; 
            set; 
        }
    }
}