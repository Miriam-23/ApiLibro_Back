using ApiLibro.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiLibro
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de Web API
            var cors = new EnableCorsAttribute("https://localhost:44367", "*", "*");
            config.EnableCors(cors);

            config.MessageHandlers.Add(new PreflightRequestsHandler());
            // Rutas de Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.XmlFormatter.UseXmlSerializer = true;
        }
    }
}
