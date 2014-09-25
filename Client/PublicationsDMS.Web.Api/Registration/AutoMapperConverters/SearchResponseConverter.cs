using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PublicationsDMS.Entities.Interfaces.Models;
using PublicationsDMS.Entities.Models;
using PublicationsDMS.Web.Api.Models;
using PublicationsDMS.Entities.Interfaces.Services;
using PublicationsDMS.Web.Api.Security;

namespace PublicationsDMS.Web.Api.Registration.AutoMapperConverters
{
    public class SearchResponseConverter : ITypeConverter<INode, SearchDocumentResponse>
    {
        private readonly INodeService _nodeService;

        public SearchResponseConverter(INodeService nodeService)
        {
            _nodeService = nodeService;
        }

        public SearchDocumentResponse Convert(ResolutionContext context)
        {
            var sourceNode = context.SourceValue as INode;
            SearchDocumentResponse resultNode = null;

            if (sourceNode != null)
            {
                resultNode = new SearchDocumentResponse();

                resultNode.ID = sourceNode.ID;
                resultNode.ParentFolderID = sourceNode.ParentFolderID;
                resultNode.Title = sourceNode.Title;

                List<string> path = new List<string>();

                var userIdentity = (PublicationsIdentity)HttpContext.Current.User.Identity;
                if (userIdentity.IsAdministrator)
                {
                    resultNode.Path = _nodeService.GetAllParents(sourceNode.ID).Select(p => p.Title);
                }
                else
                {
                    resultNode.Path = _nodeService.GetParents(sourceNode.ID, userIdentity.UserID).Select(p => p.Title);
                }
            }

            return resultNode;
        }
    }
}