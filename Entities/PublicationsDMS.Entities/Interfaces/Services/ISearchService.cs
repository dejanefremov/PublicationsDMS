using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicationsDMS.Entities.Models;

namespace PublicationsDMS.Entities.Interfaces.Services
{
    public interface ISearchService
    {
        void IndexDocuments(IEnumerable<Document> documents);

        IEnumerable<Guid> SearchDocuments(string criteria);
    }
}
