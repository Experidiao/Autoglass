using Microsoft.EntityFrameworkCore;
using Autoglass.Domain.Models;
using Autoglass.Infra.Data.Mappings;
using Infra.Data.Mappings;

namespace Autoglass.Infra.Data.Context
{
    public class AutoglassContext : DbContext
    {
        public AutoglassContext(DbContextOptions<AutoglassContext> options)
            : base(options)
        {
        }

        public DbSet<User> TblUser { get; set; }
        public DbSet<Produto> TblProduto { get; set; }
        public DbSet<Cliente> TblCliente { get; set; }
        public DbSet<ClienteXProduto> TblClienteXProduto { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // usado para ativar o lazy Loading
            // Juntamente com o pacote Microsoft.EntityFrameworkCore.Proxies
         //   optionsBuilder.UseLazyLoadingProxies();
             base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapear relacão entre objetos
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new ClienteXProdutoMap());
            modelBuilder.ApplyConfiguration(new UserMap());

            base.OnModelCreating(modelBuilder);
        }


    }
}
