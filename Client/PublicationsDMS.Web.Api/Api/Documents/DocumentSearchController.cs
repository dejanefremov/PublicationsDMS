using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PublicationsDMS.Entities.Interfaces.Services;
using PublicationsDMS.Entities.Models;
using PublicationsDMS.Web.Api.Models;
using PublicationsDMS.Web.Api.Attributes;
using PublicationsDMS.Web.Api.Security;
using System.Web;
using AutoMapper;

namespace PublicationsDMS.Web.Api.Documents
{
    [UserAuthorize]
    public class DocumentSearchController : ApiController
    {
        private readonly IDocumentService _documentService;

        public DocumentSearchController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public IEnumerable<SearchDocumentResponse> Get([FromUri] SearchDocumentRequest value)
        {
            var userIdentity = (PublicationsIdentity)HttpContext.Current.User.Identity;

            return Mapper.Map<System.Collections.Generic.IEnumerable<SearchDocumentResponse>>(_documentService.SearchDocuments(value.SearchingTerm, value.ParentFolderID, userIdentity.UserID));
        }
    }
}
