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

namespace PublicationsDMS.Web.Api.Users
{
    [AdminAuthorize]
    public class UserListController : ApiController
    {
        private readonly IUserService _userService;

        public UserListController(IUserService userService)
        {
            _userService = userService;
        }

        public IEnumerable<User> Get()
        {
            return _userService.GetAll();
        }
    }
}
