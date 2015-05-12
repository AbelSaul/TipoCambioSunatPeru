using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Peru.Sunat.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{anho}/{mes}/{dia}",
                defaults: new { dia = RouteParameter.Optional }
            );
        }
    }
}
