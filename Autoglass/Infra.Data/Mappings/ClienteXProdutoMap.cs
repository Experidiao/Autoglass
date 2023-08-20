using Autoglass.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Infra.Data.Mappings
{
    public  class ClienteXProdutoMap : IEntityTypeConfiguration<ClienteXProduto>
    {
        public void Configure(EntityTypeBuilder<ClienteXProduto> builder)
        {
            builder.HasKey(c => c.IdClienteXProduto);
            builder.HasOne(x => x.clientes).WithMany(x => x.ClienteXProdutos).HasForeignKey(x => x.IdCliente).OnDelete(DeleteBehavior.Restrict);
          //  builder.HasOne(x => x.produtos).WithMany(x => x.ClienteXProdutos).HasForeignKey(x => x.IdProduto).OnDelete(DeleteBehavior.Restrict);
        }
    }
}


