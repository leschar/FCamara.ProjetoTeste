using System;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;

[assembly: OwinStartup(typeof(FCamara.ProjetoTeste.WebApi.Autenticacao.Startup))]

namespace FCamara.ProjetoTeste.WebApi.Autenticacao
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            var myProvider = new AutorizacaoServerProvider();
            var options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(1),
                Provider = myProvider
            };
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());


            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
    }
}