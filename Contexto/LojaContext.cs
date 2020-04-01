using Alura.Loja.Testes.ConsoleApp.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Alura.Loja.Testes.ConsoleApp.Contexto
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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LojaContext>(options => options.UseSqlServer(@"Server=DESKTOP-USFG9FR\SQLSERVER2016;Database=LojaDB;Trusted_Connection=true;"));
        }  
    }
}