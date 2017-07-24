using System;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;
using FCamara.ProjetoTeste.WebApi.Produto.Helpers;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using FCamara.ProjetoTeste.Ioc;
using FCamara.ProjetoTeste.WebApi.Produto;

[assembly: OwinStartup(typeof(FCamara.ProjetoTeste.WebApi.Produto.Startup))]


namespace FCamara.ProjetoTeste.WebApi.Produto
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            // Configure Dependency Injection
            var container = new UnityContainer();
            DependencyResolver.Resolve(container);
            config.DependencyResolver = new UnityResolver(container);

            ConfigureWebApi(config);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(1)
            };
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            app.UseWebApi(config);
        }

        public static void ConfigureWebApi(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
        
    }
}