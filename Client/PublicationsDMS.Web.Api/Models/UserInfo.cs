using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PublicationsDMS.Entities.Models;

namespace PublicationsDMS.Web.Api.Models
{
    public class UserInfo
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdministrator { get; set; }
    }
}