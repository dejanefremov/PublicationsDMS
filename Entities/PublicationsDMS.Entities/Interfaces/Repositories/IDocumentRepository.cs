using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicationsDMS.Entities.Models;

namespace PublicationsDMS.Entities.Interfaces.Repositories
{
    public interface IDocumentRepository : IDisposable
    {
        Document GetByID(int documentID);

        IEnumerable<Document> GetByParentID(int? parentID);

        void Save(Document document);

        void UpdateDocument(Document document);

        void AddNewDocuments(List<Document> documents);

        IEnumerable<Document> GetByFileIDList(IEnumerable<Guid> documentFileIDs);
    }
}
