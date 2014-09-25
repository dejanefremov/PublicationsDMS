using AutoMapper;
using PublicationsDMS.Web.Api.Attributes;
using PublicationsDMS.Web.Api.Models;
using PublicationsDMS.Web.Api.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PublicationsDMS.Web.Api.Authentication
{
    [UserAuthorize]
    public class UserInfoController : ApiController
    {
        public UserInfo Get()
        {
            var user = ((PublicationsIdentity)HttpContext.Current.User.Identity);
            return new UserInfo
            {
                UserID = user.UserID,
                Name = user.Name,
                Email = user.Email,
                IsAdministrator = user.IsAdministrator
            };
        }
    }
}
