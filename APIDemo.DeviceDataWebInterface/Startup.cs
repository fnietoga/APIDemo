using System;
using System.Web.Http;
using Owin;
using Swashbuckle.Application;
using Swashbuckle.Swagger.XmlComments;

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

            config.EnableSwagger(c => 
            {
                c.SingleApiVersion("v1", "Kabel - APIDemo. Tu API en 1 hora.");
                c.IncludeXmlComments("MyAPI.xml");              
                //c.OperationFilter<MultipleOperationsWithSameVerbFilter>();
                //c.IgnoreObsoleteActions();
                //c.DescribeAllEnumsAsStrings();
                //c.IgnoreObsoleteProperties();
             

            }) 
            .EnableSwaggerUi();
          
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
