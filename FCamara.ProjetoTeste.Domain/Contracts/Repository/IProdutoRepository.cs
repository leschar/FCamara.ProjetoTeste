using System.Collections.Generic;
using FCamara.ProjetoTeste.Domain.Models;

namespace FCamara.ProjetoTeste.Domain.Contracts.Repository
{
    public interface IProdutoRepository
    {
        void Adicionar(Produto produto);
        IEnumerable<Produto> ObterTodos();
    }
}
