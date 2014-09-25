using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using PublicationsDMS.Services.Registration;
using PublicationsDMS.Web.Api.Registration;
using System.Web.Mvc;
using Autofac.Integration.Mvc;


namespace PublicationsDMS.Web.Api
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutofacInitialize();
            
            AppSettings.Initialize(System.Web.HttpContext.Current.Server.MapPath("~"));

            WebApiConfig.Register(GlobalConfiguration.Configuration);
        }

        private void AutofacInitialize()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new DomainModule());
            builder.RegisterModule(new SearchModule());
            builder.RegisterModule(new StorageModule());


            IContainer container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            PublicationsDMS.Web.Api.Registration.AutoMapperInitializer.Initialize(container);
            PublicationsDMS.Services.Registration.AutoMapperInitializer.Initialize();
        }
    }
}