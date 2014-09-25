using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PublicationsDMS.Entities.Interfaces.Models;
using PublicationsDMS.Entities.Interfaces.Services;
using PublicationsDMS.Web.Api.Attributes;
using PublicationsDMS.Entities.Models;
using System.Web;
using PublicationsDMS.Web.Api.Security;

namespace PublicationsDMS.Web.Api.Folders
{
    [UserAuthorize]
    public class BreadcrumbNodeController : ApiController
    {
        private readonly INodeService _nodeService;

        public BreadcrumbNodeController(INodeService nodeService)
        {
            _nodeService = nodeService;
        }

        public IList<DataItem> Get(int parentID)
        {
            var userIdentity = (PublicationsIdentity)HttpContext.Current.User.Identity;

            if (userIdentity.IsAdministrator)
            {
                return _nodeService.GetAllParents(parentID);
            }

            return _nodeService.GetParents(parentID, userIdentity.UserID);
        }
    }
}
