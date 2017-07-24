using System;

namespace FCamara.ProjetoTeste.Domain.Models
{
    public class Produto
    {
        protected Produto()
        {
            
        }

        public Produto(string nome)
        {
            Id = Guid.NewGuid();
            Nome = nome;
        }
        public Guid Id { get;private set; }
        public string Nome { get;private set; }
    }
}
