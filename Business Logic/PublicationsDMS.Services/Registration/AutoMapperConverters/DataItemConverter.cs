using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PublicationsDMS.Entities.Models;
using DomainDataItem = PublicationsDMS.Domain.Models.DataItem;
using PublicationsDMS.Entities.Enumerations;

namespace PublicationsDMS.Services.Registration.AutoMapperConverters
{
    public class DataItemConverter : ITypeConverter<DomainDataItem, DataItem>
    {
        public Entities.Models.DataItem Convert(ResolutionContext context)
        {
            var sourceDataItem = context.SourceValue as DomainDataItem;
            DataItem resultDataItem = null;

            if (sourceDataItem != null)
            {
                resultDataItem = new DataItem();

                resultDataItem.ID = sourceDataItem.DataItemID;
                resultDataItem.ParentFolderID = sourceDataItem.ParentFolderID;
                resultDataItem.Title = sourceDataItem.Title;
                resultDataItem.Type = (DataItemType)sourceDataItem.Type;
            }

            return resultDataItem;
        }
    }
}
