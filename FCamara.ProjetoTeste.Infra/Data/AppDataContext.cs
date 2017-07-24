using System.Data.Entity;
using FCamara.ProjetoTeste.Domain.Models;
using FCamara.ProjetoTeste.Infra.Data.Map;

namespace FCamara.ProjetoTeste.Infra.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext() : base("FCamaraConnectionString")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProdutoMap());
        }
    }
}
