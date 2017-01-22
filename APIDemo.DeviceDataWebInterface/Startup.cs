﻿using System.Web.Http;
using Owin;

namespace APIDemo.DeviceDataWebInterface
{
    public static class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public static void ConfigureApp(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
                   
            // Attribute routing.
            config.MapHttpAttributeRoutes();
            config.EnsureInitialized();

            //Default routing
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );



            appBuilder.UseWebApi(config);
        }
    }
}
