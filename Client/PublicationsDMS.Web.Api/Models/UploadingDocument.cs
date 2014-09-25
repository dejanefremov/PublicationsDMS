using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicationsDMS.Web.Api.Models
{
    public class UploadingDocument
    {
        public Guid FileID { get; set; }
        public string Title { get; set; }
    }
}