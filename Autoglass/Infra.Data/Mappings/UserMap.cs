using Autoglass.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(c => c.IdUser);
        // obrigatorio a descricão do produto
        builder.Property(c => c.UserName).HasMaxLength(30).IsRequired();

    }
}
}
