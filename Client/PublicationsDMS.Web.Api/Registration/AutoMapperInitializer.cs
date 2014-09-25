using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PublicationsDMS.Entities.Interfaces.Models;
using PublicationsDMS.Entities.Models;
using PublicationsDMS.Web.Api.Models;
using PublicationsDMS.Web.Api.Registration.AutoMapperConverters;
using Autofac;

namespace PublicationsDMS.Web.Api.Registration
{
    public class AutoMapperInitializer
    {
        public static void Initialize(IContainer container)
        {
            Mapper.Configuration.ConstructServicesUsing(type => container.Resolve(type));
            container.Resolve<IEnumerable<Profile>>().ToList().ForEach(Mapper.AddProfile);

            Mapper.CreateMap<INode, Node>().ConvertUsing<NodeConverter>();
            Mapper.CreateMap<INode, SearchDocumentResponse>().ConvertUsing<SearchResponseConverter>();
        }
    }
}