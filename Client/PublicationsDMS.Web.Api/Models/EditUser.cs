using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PublicationsDMS.Entities.Models;

namespace PublicationsDMS.Web.Api.Models
{
    public class EditUser
    {
        public User UserData { get; set; }
        public string Password { get; set; }
    }
}