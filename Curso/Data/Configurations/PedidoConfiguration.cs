using Curso.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Curso.Data.Configurations
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IniciadoEm).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            builder.Property(x => x.StatusPedido).HasConversion<string>();
            builder.Property(x => x.TipoFrete).HasConversion<int>();
            builder.Property(x => x.Observacao).HasColumnType("VARCHAR(512)");

            //definindo o relacionamento da tabela
            // Um pedido tem muitos itens e um item de pedido pertence a apenas um pedido
            builder.HasMany(x => x.Itens)
                .WithOne(x => x.Pedido)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}