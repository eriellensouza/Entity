using Alura.EntityFramework.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Alura.EntityFramework.Contexto
{
    public class LojaContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(@"Server=DESKTOP-USFG9FR\SQLSERVER2016;Database=LojaDB;Trusted_Connection=true;");
            }
        }

        public LojaContext()
        {
        }
        public LojaContext(DbContextOptions<LojaContext> options)
            :base(options)
        {}
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Promocao> Promocoes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<PromocaoProduto>()
                .HasKey(pp => new { pp.ProdutoId, pp.PromocaoId });

            modelBuilder
                .Entity<Endereco>()
                .ToTable("Enderecos");


            modelBuilder
                .Entity<Endereco>()
                .Property<int>("ClienteId");
            modelBuilder
                .Entity<Endereco>()
                .HasKey("ClienteId");
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LojaContext>(options => options.UseSqlServer(@"Server=DESKTOP-USFG9FR\SQLSERVER2016;Database=LojaDB;Trusted_Connection=true;"));
        }  
    }
}