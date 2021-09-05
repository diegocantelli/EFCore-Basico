using Curso.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Curso.Data.Configurations
{
    public class PedidoItemConfiguration : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.ToTable("PedidoItens");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Quantidade).HasDefaultValue(1).IsRequired();
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Desconto).IsRequired();
        }
    }
}