using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using PublicationsDMS.Entities.Interfaces.Services;
using PublicationsDMS.Search;
using PublicationsDMS.Storage;

namespace PublicationsDMS.Services.Registration
{
    public class StorageModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileSystemStorageService>().As<IStorageService>();
        }
    }
}
