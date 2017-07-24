using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace FCamara.ProjetoTeste.WebApi.Autenticacao.Controllers
{
    public class UsuarioController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("api/login/home")]
        public IHttpActionResult Get()
        {
            return Ok("Hora atual é: " + DateTime.Now);
        }

        [Authorize]
        [HttpGet]
        [Route("api/login/autenticacao")]
        public IHttpActionResult GetForAuthenticate()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("Olá " + identity.Name);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("api/login/autorizacao")]
        public IHttpActionResult GetForAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value);
            return Ok("Olá " + identity.Name + " Permissão: " + string.Join(",", roles.ToList()));
        }
    }
}
