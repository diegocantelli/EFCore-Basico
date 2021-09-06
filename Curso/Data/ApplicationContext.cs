using System;
using Curso.Data.Configurations;
using Curso.Domain;
using Microsoft.EntityFrameworkCore;

namespace Curso.Data
{
    public class ApplicationContext : DbContext
    {
        //Definindo a classe Pedido como modelo de dados, o EF Core irá criar também as classes
        // na qual ela depende
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data source=(localdb)\\mssqllocaldb;Initial Catalog=CursoEFCore; Integrated Security=true",
            x => x.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(2), errorNumbersToAdd: null));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // aplicando as configurações definidas em ClienteConfiguration para criar a tabela 
        //    modelBuilder.ApplyConfiguration(new ClienteConfiguration());

            // irá aplicar as configurações com base nas classes quem implementem IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }
    }
}