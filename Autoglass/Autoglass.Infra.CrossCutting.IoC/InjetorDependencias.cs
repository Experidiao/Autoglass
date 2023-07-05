using Autoglass.Application.Interface;
using Autoglass.Application.Services;
using Autoglass.Domain.Interfaces;
using Infra.Data.Repository;
using Infra.Data.UoW;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autoglass.Infra.Data.Context;

namespace Autoglass.Infra.CrossCutting.IoC
{
    public static class InjetorDependencias
    {
        public static void RegistrarDependencia(IServiceCollection services)
        {
            // application 
            services.AddScoped<IProdutoApplication, ProdutoApplication>();

            // infra
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            // contexto
            services.AddScoped<AutoglassContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
