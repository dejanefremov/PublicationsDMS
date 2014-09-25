using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using PublicationsDMS.Entities.Interfaces.Services;
using PublicationsDMS.Search;

namespace PublicationsDMS.Services.Registration
{
    public class SearchModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SearchService>().As<ISearchService>();
        }
    }
}
