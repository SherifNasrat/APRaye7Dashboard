using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace APRaye7
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            "ChangeLogs",                                              // Route name
            "{controller}/{action}/{id}/{RowID}",                           // URL with parameters
            new { controller = "ChangeLogs", action = "Details", id = "", RowID = "" }  // Parameter defaults
        );
        }
    }
}
