using FCamara.ProjetoTeste.Domain.Models;
using System.Collections.Generic;

namespace FCamara.ProjetoTeste.Domain.Contracts.Service
{
    public interface IProdutoService
    {
        void Adicionar(Produto produto);
        IEnumerable<Produto> ObterTodos();
    }
}
