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

namespace PublicationsDMS.Web.Api.Documents
{
    [AdminAuthorize]
    public class DocumentEditController : ApiController
    {
        private readonly IDocumentService _documentService;

        public DocumentEditController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public void Post(Document document)
        {
            if (document != null)
            {
                _documentService.UpdateDocument(document);
            }
        }
    }
}
