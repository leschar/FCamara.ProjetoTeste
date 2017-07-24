using System.Collections.Generic;
using FCamara.ProjetoTeste.Domain.Contracts.Repository;
using FCamara.ProjetoTeste.Domain.Models;
using FCamara.ProjetoTeste.Infra.Data;

namespace FCamara.ProjetoTeste.Infra.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private AppDataContext _context;
        public ProdutoRepository(AppDataContext context)
        {
            _context = context;
        }
        public void Adicionar(Produto produto)
        {
            _context.Set<Produto>().Add(produto);
            _context.SaveChanges();
        }

        public IEnumerable<Produto> ObterTodos()
        {
            return _context.Set<Produto>();
        }
    }
}
