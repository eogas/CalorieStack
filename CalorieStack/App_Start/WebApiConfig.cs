using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace CalorieStack
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // We want all json to be camelCase instead of the default BumpyCase
            var jsonConfig = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonConfig.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Temporary hack, probably should have date specified in param list
            config.Routes.MapHttpRoute(
                name: "DefaultApi2",
                routeTemplate: "api/Days/{stackId}/{year}/{month}/{dayOfMonth}",
                defaults: new {
                    controller="Days",
                    id = RouteParameter.Optional
                }
            );
        }
    }
}
