using Microsoft.EntityFrameworkCore;
using Autoglass.Domain.Models;
using Autoglass.Infra.Data.Mappings;

namespace Autoglass.Infra.Data.Context
{
    public class AutoglassContext : DbContext
    {
        public AutoglassContext(DbContextOptions<AutoglassContext> options)
            : base(options)
        {
        }
        public DbSet<Produto> TblProduto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new ProdutoMap());
            base.OnModelCreating(modelBuilder);
        }

    }
}
