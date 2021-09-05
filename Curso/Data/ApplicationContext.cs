using Curso.Domain;
using Microsoft.EntityFrameworkCore;

namespace Curso.Data
{
    public class ApplicationContext : DbContext
    {
        //Definindo a classe Pedido como modelo de dados, o EF Core irá criar também as classes
        // na qual ela depende
        // public DbSet<Pedido> Pedidos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data source=(localdb)\\mssqllocaldb: Initial Catalog=CursoEFCore; Integrated Security=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //equivalente ao DbSet<>
            modelBuilder.Entity<Cliente>(x => 
            {
                x.ToTable("Clientes");
                x.HasKey(x => x.Id);
                x.Property(x => x.Nome).HasColumnType("VARCHAR(80)").IsRequired();
                x.Property(x => x.Telefone).HasColumnType("CHAR(8)");
                x.Property(x => x.CEP).HasColumnType("CHAR(8)").IsRequired();
                x.Property(x => x.Estado).HasColumnType("CHAR(2)").IsRequired();
                x.Property(x => x.Cidade).HasMaxLength(60).IsRequired();

                x.HasIndex(x => x.Telefone).HasName("id_cliente_telefone");
            });

            modelBuilder.Entity<Produto>(x => 
            {
                x.ToTable("Produtos");
                x.HasKey(x => x.Id);
                x.Property(x => x.CodigoBarras).HasColumnType("VARCHAR(14)").IsRequired();
                x.Property(x => x.Descricao).HasColumnType("VARCHAR(60)");
                x.Property(x => x.Valor).IsRequired();
                x.Property(x => x.TipoProduto).HasConversion<string>();    
            });

            modelBuilder.Entity<Pedido>(x => 
            {
                x.ToTable("Pedidos");
                x.HasKey(x => x.Id);
                x.Property(x => x.IniciadoEm).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
                x.Property(x => x.StatusPedido).HasConversion<string>();
                x.Property(x => x.TipoFrete).HasConversion<int>();
                x.Property(x => x.Observacao).HasColumnType("VARCHAR(512)");

                //definindo o relacionamento da tabela
                // Um pedido tem muitos itens e um item de pedido pertence a apenas um pedido
                x.HasMany(x => x.Itens)
                    .WithOne(x => x.Pedido)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PedidoItem>(x => 
            {
                x.ToTable("PedidoItens");
                x.HasKey(x => x.Id);
                x.Property(x => x.Quantidade).HasDefaultValue(1).IsRequired();
                x.Property(x => x.Valor).IsRequired();
                x.Property(x => x.Desconto).IsRequired();
            });
        }
    }
}