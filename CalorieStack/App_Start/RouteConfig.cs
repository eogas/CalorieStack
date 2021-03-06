﻿using System.Web.Mvc;
using System.Web.Routing;

namespace CalorieStack
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Stack",
                url: "{id}",
                defaults: new { controller = "Home", action = "Stack" }
            );

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
