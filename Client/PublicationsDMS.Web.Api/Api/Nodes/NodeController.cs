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
using PublicationsDMS.Web.Api.Security;
using System.Web;

namespace PublicationsDMS.Web.Api.Nodes
{
    [UserAuthorize]
    public class NodeController : ApiController
    {
        private readonly INodeService _nodeService;

        public NodeController(INodeService nodeService)
        {
            _nodeService = nodeService;
        }

        public Node Get(int nodeID)
        {
            var userIdentity = (PublicationsIdentity)HttpContext.Current.User.Identity;

            INode node = _nodeService.GetByID(nodeID, userIdentity.UserID);

            return Mapper.Map<Node>(node);
        }
    }
}
