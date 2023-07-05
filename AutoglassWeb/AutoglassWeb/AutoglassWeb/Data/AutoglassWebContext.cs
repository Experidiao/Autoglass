using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoglassWeb.Models;

namespace AutoglassWeb.Data
{
    public class AutoglassWebContext : DbContext
    {
        public AutoglassWebContext (DbContextOptions<AutoglassWebContext> options)
            : base(options)
        {
        }

        public DbSet<AutoglassWeb.Models.Produto> Produto { get; set; }
    }
}
