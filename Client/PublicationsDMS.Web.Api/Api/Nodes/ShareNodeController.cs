using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PublicationsDMS.Entities.Interfaces.Models;
using PublicationsDMS.Entities.Interfaces.Services;
using PublicationsDMS.Web.Api.Models;
using PublicationsDMS.Web.Api.Attributes;
using PublicationsDMS.Entities.Models;

namespace PublicationsDMS.Web.Api.Nodes
{
    [AdminAuthorize]
    public class ShareNodeController : ApiController
    {
        private readonly INodeService _nodeService;
        private readonly IUserService _userService;

        public ShareNodeController(INodeService nodeService, IUserService userService)
        {
            _nodeService = nodeService;
            _userService = userService;
        }

        public IEnumerable<User> Get(int nodeID)
        {
            return _nodeService.GetNodeUsers(nodeID);
        }

        public User Put(string userEmail)
        {
            return _userService.GetByEmail(userEmail);
        }

        public void Post(ShareNodeUserList value)
        {
            _nodeService.SaveDataItemUsers(value.NodeID, value.UserIDs);
        }
    }
}
