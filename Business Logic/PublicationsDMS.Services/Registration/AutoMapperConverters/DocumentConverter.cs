using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PublicationsDMS.Entities.Models;
using DomainDocument = PublicationsDMS.Domain.Models.Document;
using PublicationsDMS.Entities.Enumerations;

namespace PublicationsDMS.Services.Registration.AutoMapperConverters
{
    public class DocumentConverter : ITypeConverter<DomainDocument, Document>
    {
        public Entities.Models.Document Convert(ResolutionContext context)
        {
            var sourceDocument = context.SourceValue as DomainDocument;
            Document resultDocument = null;

            if (sourceDocument != null)
            {
                resultDocument = new Document();

                resultDocument.ID = sourceDocument.DocumentID;
                resultDocument.ParentFolderID = sourceDocument.DataItem.ParentFolderID;
                resultDocument.Title = sourceDocument.DataItem.Title;
                resultDocument.Type = (DataItemType)sourceDocument.DataItem.Type;

                resultDocument.FileExtension = sourceDocument.FileExtension;
                resultDocument.FileID = sourceDocument.FileID;
                resultDocument.Description = sourceDocument.Description;
            }

            return resultDocument;
        }
    }
}
