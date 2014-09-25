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
using System.Web;
using PublicationsDMS.Web.Api.Security;
using PublicationsDMS.Entities.Enumerations;

namespace PublicationsDMS.Web.Api.Nodes
{
    [UserAuthorize]
    public class NodeListController : ApiController
    {
        private readonly INodeService _nodeService;

        public NodeListController(INodeService nodeService)
        {
            _nodeService = nodeService;
        }

        public IEnumerable<Node> Get(int? parentID)
        {
            var userIdentity = (PublicationsIdentity)HttpContext.Current.User.Identity;

            List<Node> result = new List<Node>();

            if (parentID.HasValue)
            {
                var parent = _nodeService.GetParentFolder(parentID.Value, userIdentity.UserID);

                if (parent != null)
                {
                    result.Add(new Node { ID = parent.ID, Title = ". .", ParentFolderID = parent.ParentFolderID, TypeName = DataItemType.Folder.ToString() });
                }
                else
                {
                    result.Add(new Node { ID = null, Title = ". .", ParentFolderID = null, TypeName = DataItemType.Folder.ToString() });
                }
            }

            IEnumerable<INode> nodes;
            
            if (userIdentity.IsAdministrator)
            {
                nodes = _nodeService.GetAllByParentID(parentID);
            }
            else
            {
                nodes = _nodeService.GetByParentID(parentID, userIdentity.UserID);
            }

            result.AddRange(Mapper.Map<System.Collections.Generic.IEnumerable<Node>>(nodes));

            return result.OrderByDescending(r => r.TypeName).ThenBy(r => r.ID);
        }
    }
}
