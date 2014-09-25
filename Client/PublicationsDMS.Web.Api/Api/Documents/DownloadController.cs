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
using System.IO;

namespace PublicationsDMS.Web.Api.Documents
{
    [UserAuthorize]
    public class DownloadController : ApiController
    {
        private readonly IDocumentService _documentService;
        private readonly IFolderService _folderService;

        public DownloadController(IDocumentService documentService, IFolderService folderService)
        {
            _documentService = documentService;
            _folderService = folderService;
        }

        public HttpResponseMessage Get(int documentID)
        {
            Document document = _documentService.GetByID(documentID);

            if (document != null)
            {
                string filePath = string.Format("{0}\\{1}", _folderService.GenerateFolderPath(document.ParentFolderID), document.FileID);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(new FileStream(filePath, FileMode.Open, FileAccess.Read));
                response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.Add("x-filename", document.Title);
                response.Content.Headers.Add("Access-Control-Expose-Headers", "x-filename");

                return response;
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}
