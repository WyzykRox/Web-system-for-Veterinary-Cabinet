using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Weterzynarze.DAL;

namespace Weterzynarze
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            /* routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

             routes.MapRoute(
                 "Test",                                              // Route name
                 "{controller}/{action}/{id}",                           // URL with parameters
                 new { controller = "Profiles", action = "GetTest", id = "0" }  // Parameter defaults
             );
             */
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}
