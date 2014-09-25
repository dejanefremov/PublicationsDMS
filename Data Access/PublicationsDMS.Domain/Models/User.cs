using System;
using System.Collections.Generic;

namespace PublicationsDMS.Domain.Models
{
    public partial class User
    {
        public User()
        {
            this.UserDataItemPermissions = new List<UserDataItemPermission>();
        }

        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdministrator { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<UserDataItemPermission> UserDataItemPermissions { get; set; }
    }
}
