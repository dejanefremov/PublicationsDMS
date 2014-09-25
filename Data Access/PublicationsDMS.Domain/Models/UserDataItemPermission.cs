using System;
using System.Collections.Generic;

namespace PublicationsDMS.Domain.Models
{
    public partial class UserDataItemPermission
    {
        public int UserDataItemPermissionID { get; set; }
        public int UserID { get; set; }
        public int DataItemID { get; set; }
        public virtual DataItem DataItem { get; set; }
        public virtual User User { get; set; }
    }
}
