using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PublicationsDMS.Entities.Models;

namespace PublicationsDMS.Web.Api.Models
{
    public class LoginUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}