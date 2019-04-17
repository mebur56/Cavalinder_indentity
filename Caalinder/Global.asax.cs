using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using Caalinder.IoC;

namespace Caalinder
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            BootStrapper.Register(container, Lifestyle.Scoped);
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            AutoMapperConfig.RegisterMappings();
        }
    }
}
