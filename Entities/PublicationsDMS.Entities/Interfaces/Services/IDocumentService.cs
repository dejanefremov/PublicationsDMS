using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicationsDMS.Entities.Models;

namespace PublicationsDMS.Entities.Interfaces.Services
{
    public interface IDocumentService
    {
        Document GetByID(int documentID);

        void Save(Document document);

        Guid SaveTempFile(Stream uploadedStream);

        void AddNewDocuments(int? parentFolderID, List<Document> documents);

        IEnumerable<Document> SearchDocuments(string criteria, int? parentID, int userID);

        void UpdateDocument(Document document);
    }
}
