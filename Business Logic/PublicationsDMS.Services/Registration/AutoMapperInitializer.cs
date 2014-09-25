using Autofac;
using AutoMapper;
using System;

namespace PublicationsDMS.Services.Registration
{
    public class AutoMapperInitializer
    {
        public static void Initialize()
        {
            Mapper.CreateMap<Domain.Models.DataItem, Entities.Models.DataItem>().ConvertUsing<PublicationsDMS.Services.Registration.AutoMapperConverters.DataItemConverter>();
            Mapper.CreateMap<Domain.Models.Folder, Entities.Models.Folder>().ConvertUsing<PublicationsDMS.Services.Registration.AutoMapperConverters.FolderConverter>();
            Mapper.CreateMap<Domain.Models.Document, Entities.Models.Document>().ConvertUsing<PublicationsDMS.Services.Registration.AutoMapperConverters.DocumentConverter>();
            Mapper.CreateMap<Domain.Models.User, Entities.Models.User>();
        }
    }
}
