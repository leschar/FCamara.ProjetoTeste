using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FCamara.ProjetoTeste.WebApi.Autenticacao
{
    public class AutorizacaoServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context) => context.Validated();

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if (context.UserName == "charles" && context.Password == "123")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                identity.AddClaim(new Claim("username", "admin"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "Charles"));
                context.Validated(identity);
            }
            else
            {
                context.SetError("permissao_invalida", "Usuário ou senha inválidos");
                return;
            }
        }
    }
}