using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicationsDMS.Web.Api.Models
{
    public class Node
    {
        public int? ID { get; set; }
        public string Title { get; set; }
        public int? ParentFolderID { get; set; }
        public string TypeName { get; set; }
    }
}