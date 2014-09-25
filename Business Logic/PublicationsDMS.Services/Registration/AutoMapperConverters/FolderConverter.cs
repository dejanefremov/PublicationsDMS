using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PublicationsDMS.Entities.Models;
using DomainFolder = PublicationsDMS.Domain.Models.Folder;
using PublicationsDMS.Entities.Enumerations;

namespace PublicationsDMS.Services.Registration.AutoMapperConverters
{
    public class FolderConverter : ITypeConverter<DomainFolder, Folder>
    {
        public Entities.Models.Folder Convert(ResolutionContext context)
        {
            var sourceFolder = context.SourceValue as DomainFolder;
            Folder resultFolder = null;

            if (sourceFolder != null)
            {
                resultFolder = new Folder();

                resultFolder.ID = sourceFolder.FolderID;
                resultFolder.ParentFolderID = sourceFolder.DataItem.ParentFolderID;
                resultFolder.Title = sourceFolder.DataItem.Title;
                resultFolder.Type = (DataItemType)sourceFolder.DataItem.Type;
            }

            return resultFolder;
        }
    }
}
