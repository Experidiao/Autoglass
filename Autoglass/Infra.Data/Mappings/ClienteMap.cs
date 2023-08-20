using Autoglass.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Infra.Data.Mappings
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.IdCliente);

            // criar registro quando a tabela estiver vazia
            builder.HasData(
                new Cliente
                {
                    IdCliente = 1,
                    Nome = "Speed",
                    CpfCnpj = "45525544"
                },
                new Cliente
                {
                    IdCliente = 2,
                    Nome = "Plinio",
                    CpfCnpj = "33333"
                });
        }
    }
}



