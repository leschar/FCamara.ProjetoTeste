using System;
using FCamara.ProjetoTeste.Domain.Contracts.Service;
using System.Collections.Generic;
using System.Web.Http;

namespace FCamara.ProjetoTeste.WebApi.Produto.Controllers
{
    public class ProdutoController : ApiController
    {
        private readonly IProdutoService _service;

        public ProdutoController(IProdutoService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/produtos/lista")]
        public IEnumerable<Domain.Models.Produto> Get()
        {
            var produto = _service.ObterTodos();
            return produto;
        }
        
    }
}
