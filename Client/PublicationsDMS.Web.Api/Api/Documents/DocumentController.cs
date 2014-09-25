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
    public class DocumentController : ApiController
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [UserAuthorize]
        public Document Get(int documentID)
        {
            return _documentService.GetByID(documentID);
        }

        [AdminAuthorize]
        public void Put(UploadingDocumentList value)
        {
            List<Document> uploadedDocuments = value.Documents.Select(d => new Document
            {
                ParentFolderID = value.ParentFolderID,
                Title = d.Title,
                FileID = d.FileID
            }).ToList();

            _documentService.AddNewDocuments(value.ParentFolderID, uploadedDocuments);
        }

        [AdminAuthorize]
        public object Post()
        {
            List<Tuple<Guid, string>> result = new List<Tuple<Guid, string>>();

            var httpRequest = System.Web.HttpContext.Current.Request;
            for (int i = 0; i < httpRequest.Files.Count; i++)
            {
                var image = httpRequest.Files[i];
                Guid fileID = _documentService.SaveTempFile(image.InputStream);

                if (fileID != Guid.Empty)
                {
                    result.Add(new Tuple<Guid, string>(fileID, image.FileName));
                }
            }

            return result.Select(t => new UploadingDocument
            {
                FileID = t.Item1,
                Title = t.Item2
            });
        }
    }
}
