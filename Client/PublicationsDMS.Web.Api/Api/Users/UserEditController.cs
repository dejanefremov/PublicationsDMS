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
    public class UserEditController : ApiController
    {
        private readonly IUserService _userService;

        public UserEditController(IUserService userService)
        {
            _userService = userService;
        }

        [UserAuthorize]
        public User Get(int userID)
        {
            var userIdentity = (PublicationsIdentity)HttpContext.Current.User.Identity;
            if (!userIdentity.IsAdministrator && userIdentity.UserID != userID)
            {
                return null;
            }

            return _userService.GetByID(userID);
        }

        [AdminAuthorize]
        public void Put(EditUser user)
        {
            if (user.UserData.UserID == 0)
            {
                user.UserData.IsActive = true;
                user.UserData.IsAdministrator = false;

                _userService.AddUser(user.UserData, user.Password);
            }
        }

        [UserAuthorize]
        public void Post(EditUser user)
        {
            var userIdentity = (PublicationsIdentity)HttpContext.Current.User.Identity;

            if (user.UserData.UserID != 0
                && (userIdentity.IsAdministrator || userIdentity.UserID == user.UserData.UserID))
            {
                _userService.UpdateUser(user.UserData);
            }
        }
    }
}
