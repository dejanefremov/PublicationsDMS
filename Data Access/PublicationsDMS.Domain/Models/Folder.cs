using System;
using System.Collections.Generic;

namespace PublicationsDMS.Domain.Models
{
    public partial class Folder
    {
        public int FolderID { get; set; }
        public virtual DataItem DataItem { get; set; }
    }
}
