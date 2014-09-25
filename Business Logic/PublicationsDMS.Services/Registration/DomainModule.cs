using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using PublicationsDMS.Domain.Repositories;
using PublicationsDMS.Entities.Interfaces.Repositories;
using AutoMapper;
using System.Reflection;

namespace PublicationsDMS.Services.Registration
{
    public class DomainModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            builder.RegisterType<DataItemRepository>().As<IDataItemRepository>();
            builder.RegisterType<DocumentRepository>().As<IDocumentRepository>();
            builder.RegisterType<FolderRepository>().As<IFolderRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<UserDataItemRepository>().As<IUserDataItemRepository>();

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ITypeConverter<,>))
                .AsSelf()
                .InstancePerDependency();
        }
    }
}
