using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PublicationsDMS.Entities.Interfaces.Services;
using PublicationsDMS.Entities.Models;
using PublicationsDMS.Web.Api.Models;
using PublicationsDMS.Web.Api.Attributes;
using PublicationsDMS.Web.Api.Security;
using System.Web;

namespace PublicationsDMS.Web.Api.Users
{
    [UserAuthorize]
    public class ChangePasswordController : ApiController
    {
        private readonly IUserService _userService;

        public ChangePasswordController(IUserService userService)
        {
            _userService = userService;
        }

        public void Post(EditUser user)
        {
            var userIdentity = (PublicationsIdentity)HttpContext.Current.User.Identity;
            
            if (user.UserData != null 
                && user.UserData.UserID != 0
                && (userIdentity.IsAdministrator || userIdentity.UserID == user.UserData.UserID))
            {
                _userService.ChangePassword(user.UserData.UserID, user.Password);
            }
        }
    }
}
