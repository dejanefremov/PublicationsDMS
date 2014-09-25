using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicationsDMS.Web.Api.Models
{
    public class UploadingDocumentList
    {
        public int? ParentFolderID
        {
            get;
            set;
        }

        public List<UploadingDocument> Documents
        {
            get;
            set;
        }
    }
}