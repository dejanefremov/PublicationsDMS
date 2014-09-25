using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Autofac.Integration.WebApi;
using PublicationsDMS.Services;
using PublicationsDMS.Entities.Interfaces.Services;
using Module = Autofac.Module;
using AutoMapper;

namespace PublicationsDMS.Web.Api.Registration
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            builder.RegisterApiControllers(assembly).PropertiesAutowired();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => typeof(Profile).IsAssignableFrom(t))
                .As<Profile>()
                .SingleInstance();

            builder.RegisterWebApiFilterProvider(System.Web.Http.GlobalConfiguration.Configuration);

            builder.RegisterType<NodeService>().As<INodeService>();
            builder.RegisterType<DocumentService>().As<IDocumentService>();
            builder.RegisterType<FolderService>().As<IFolderService>();
            builder.RegisterType<PermissionService>().As<IPermissionService>();
            builder.RegisterType<UserService>().As<IUserService>();

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ITypeConverter<,>))
                .AsSelf()
                .InstancePerDependency();
        }
    }
}