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
using Autoglass.Domain.Authentication;
using Infra.Data.Authentication;

namespace Autoglass.Infra.CrossCutting.IoC
{
    public static class InjetorDependencias
    {
        public static void RegistrarDependencia(IServiceCollection services)
        {
            // AddTransient - sempre que solicitado abre uma nova instancia
            // application 
            services.AddTransient<IProdutoApplication, ProdutoApplication>();
            services.AddTransient<IClienteApplication, ClienteApplication>();
            services.AddTransient<IClienteXProdutoApplication, ClienteXProdutoApplication>();
            services.AddTransient<IUserApplication, UserApplication>();

            // infra
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IClienteXProdutoRepository, ClienteXProdutoRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITokenGenerator,TokenGenerator>();

            // AddScoped -- Uma vez instancia, qdo for chamado novamente utiliza a mesma instancia.
            // contexto
            services.AddScoped<AutoglassContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
