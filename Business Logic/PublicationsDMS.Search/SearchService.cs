using PublicationsDMS.Entities.Interfaces.Services;
using PublicationsDMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis.Standard;
using Version = Lucene.Net.Util.Version;
using Lucene.Net.Index;
using LuceneDocument = Lucene.Net.Documents.Document;
using Lucene.Net.Store;
using System.IO;
using Lucene.Net.Documents;
using Document = PublicationsDMS.Entities.Models.Document;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace PublicationsDMS.Search
{
    public class SearchService : ISearchService
    {
        private readonly IFolderService _folderService;

        public SearchService(IFolderService folderService)
        {
            _folderService = folderService;
        }

        private FSDirectory LuceneDirectory
        {
            get
            {
                FSDirectory _directoryTemp = FSDirectory.Open(new DirectoryInfo(SearchSettings.LuceneIndexFolder));

                if (IndexWriter.IsLocked(_directoryTemp))
                {
                    IndexWriter.Unlock(_directoryTemp);
                }

                var lockFilePath = Path.Combine(SearchSettings.LuceneIndexFolder, "write.lock");

                if (File.Exists(lockFilePath))
                {
                    File.Delete(lockFilePath);
                }

                return _directoryTemp;
            }
        }  

        public void IndexDocuments(IEnumerable<Document> documents)
        {
            try
            {
                var analyzer = new StandardAnalyzer(Version.LUCENE_30);
                bool createIndexFiles = !LuceneDirectory.FileExists("segments.gen");
                using (var writer = new IndexWriter(LuceneDirectory, analyzer, createIndexFiles, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    try
                    {
                        foreach (var document in documents.Where(d => d.FileExtension == ".pdf"))
                        {
                            string documentBody = GetPlainTextFromDocument(document);

                            var doc = new LuceneDocument();

                            doc.Add(new Field("FileID", document.FileID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                            doc.Add(new Field("Title", document.Title, Field.Store.YES, Field.Index.ANALYZED));
                            doc.Add(new Field("Body", documentBody, Field.Store.YES, Field.Index.ANALYZED));

                            writer.AddDocument(doc);
                        }

                        writer.Optimize();
                    }
                    catch { }
                    finally
                    {
                        analyzer.Close();
                    }
                }
            }
            catch { }
        }

        public IEnumerable<Guid> SearchDocuments(string criteria)
        {
            List<Guid> result = new List<Guid>();

            using (var searcher = new IndexSearcher(LuceneDirectory, false))
            {
                var analyzer = new StandardAnalyzer(Version.LUCENE_30);

                try
                {
                    criteria = string.Format("*{0}*", criteria);

                    var queryParser = new QueryParser(Version.LUCENE_30, "Body", analyzer);
                    queryParser.AllowLeadingWildcard = true;

                    var query = queryParser.Parse(criteria);
                    
                    var hits = searcher.Search(query, searcher.MaxDoc).ScoreDocs;

                    foreach (var hit in hits)
                    {
                        LuceneDocument luceneDocument = searcher.Doc(hit.Doc);
                        result.Add(Guid.Parse(luceneDocument.Get("FileID")));
                    }
                }
                catch { }

                analyzer.Close();
            }

            return result;
        }

        private string GetPlainTextFromDocument(Document document)
        {
            string plainText = string.Empty;

            try
            {
                string destinationFilePath = string.Format("{0}\\{1}", _folderService.GenerateFolderPath(document.ParentFolderID), document.FileID);

                if (document.FileExtension == ".pdf")
                {
                    PdfReader reader = new PdfReader(destinationFilePath);
                    StringWriter output = new StringWriter();
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        output.WriteLine(PdfTextExtractor.GetTextFromPage(reader, i, new SimpleTextExtractionStrategy()));
                    }

                    plainText = output.ToString();
                }
            }
            catch { }

            return plainText;
        }
    }
}
