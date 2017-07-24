using FCamara.ProjetoTeste.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FCamara.ProjetoTeste.Infra.Data.Map
{
    public class ProdutoMap : EntityTypeConfiguration<Produto>
    {
        public ProdutoMap()
        {
            ToTable("Produto");

            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Nome)
                .HasMaxLength(60)
                .IsRequired();
        }
    }
}
