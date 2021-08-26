using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using Owin;
using System;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace EKYC.DesktopTool.Api
{
    internal class SelfHostedApi : IDisposable
    {
        private IDisposable server;

        public class EkycOwinStartup
        {
            public void Configuration(IAppBuilder appBuilder)
            {
                HttpConfiguration config = new HttpConfiguration();
                config.Formatters.Clear();
                config.Formatters.Add(new JsonMediaTypeFormatter());
                config.Formatters.JsonFormatter.SerializerSettings =
                new JsonSerializerSettings
                {
                    //ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                config.Routes.MapHttpRoute(
                    name: "Api",
                    routeTemplate: "{controller}/{action}",
                    defaults: new { id = RouteParameter.Optional }
                );
                appBuilder.Use((context, next) => {
                    context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                    context.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "*" });
                    context.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "*" });
                    if (context.Request.Method == "OPTIONS")
                    {
                        context.Response.StatusCode = 200;
                        return context.Response.WriteAsync("OK");
                    }
                    return next.Invoke();
                });
                appBuilder.UseWebApi(config);
            }
        }

        public void Start()
        {
            server = WebApp.Start<EkycOwinStartup>("http://localhost:9907/");
        }

        public void Dispose()
        {
            this.server.Dispose();
        }
    }
}
