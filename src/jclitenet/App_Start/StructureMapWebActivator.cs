using CoreFramework4;
using CoreFramework4.Implementations.Security;
using CoreFramework4.Implementations.Services;
using CoreFramework4.Infrastructure.Services;
using CoreFramework4.Migration.Migration;
using CoreFramework4.Utility;
using MvcSiteMapProvider.Web;
using MvcSiteMapProvider.Web.Html;
using StructureMap;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(jclitenet.App_Start.StructureMapWebActivator), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(jclitenet.App_Start.StructureMapWebActivator), "Shutdown")]

namespace jclitenet.App_Start
{
    public static class StructureMapWebActivator
    {
        public static void Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Formatters.Clear();
            GlobalConfiguration.Configuration.Formatters.Add(new System.Net.Http.Formatting.JsonMediaTypeFormatter());

            AppVersion.UpdateToLatestVersion(new Guid("{D435073A-231B-4a6f-9FC6-FD708FD263E7}"));
            AppVersion.UpdateToLatestVersion(new Guid("{4B543CC9-7A7B-4C9F-A43A-A514B9221248}"));

            BootStrapper.Bootstrap();

            ServiceFactory.GetInstance<ISiteConfigurationService>()
                          .CacheConfigs();

            ControllerBuilder.Current.SetControllerFactory(new CoreFramework4.Mvc.StructureMapControllerFactory());

            //DependencyResolver.SetResolver(new jclitenet.Resolver.MVCDependencyResolver(ObjectFactory.Container));
            GlobalConfiguration.Configuration.DependencyResolver = new jclitenet.Resolver.DependencyResolver(ObjectFactory.Container);

            CoreFramework4.ObjectMappings.MapDomainObjects();

        }

        public static void Shutdown() 
        {
        }
    }
}