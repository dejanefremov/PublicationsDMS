using System;
using System.Collections.Generic;

namespace PublicationsDMS.Domain.Models
{
    public partial class DataItem
    {
        public DataItem()
        {
            this.DataItem1 = new List<DataItem>();
            this.UserDataItemPermissions = new List<UserDataItemPermission>();
        }

        public int DataItemID { get; set; }
        public string Title { get; set; }
        public Nullable<int> ParentFolderID { get; set; }
        public byte Type { get; set; }
        public virtual ICollection<DataItem> DataItem1 { get; set; }
        public virtual DataItem DataItem2 { get; set; }
        public virtual Document Document { get; set; }
        public virtual Folder Folder { get; set; }
        public virtual ICollection<UserDataItemPermission> UserDataItemPermissions { get; set; }
    }
}
