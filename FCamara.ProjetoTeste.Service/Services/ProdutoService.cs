using FCamara.ProjetoTeste.Domain.Contracts.Service;
using System.Collections.Generic;
using System.Linq;
using FCamara.ProjetoTeste.Domain.Contracts.Repository;
using FCamara.ProjetoTeste.Domain.Models;

namespace FCamara.ProjetoTeste.Service.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public void Adicionar(Produto produto)
        {
            _produtoRepository.Adicionar(produto);
        }

        public IEnumerable<Produto> ObterTodos()
        {
            //criei o método para ser usado via api, porem para efeitos de teste estou populando automaticamente
            foreach (var listaProduto in ListaProdutos())
            {
                if (!_produtoRepository.ObterTodos().Any(c=>c.Nome.Contains(listaProduto.Nome)))
                {
                    _produtoRepository.Adicionar(listaProduto);
                }
            }

            return _produtoRepository.ObterTodos();
        }

        private IEnumerable<Produto> ListaProdutos()
        {
            var produto = new Produto("Arroz");
            var produto1 = new Produto("Feijao");
            var produto2 = new Produto("Bife");
            var produto3 = new Produto("Batata Frita");
            var lista = new List<Produto> { produto, produto1, produto2, produto3 };
            return lista;
        }
    }
}
