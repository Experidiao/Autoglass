using Autoglass.Domain.Interfaces;
using Autoglass.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Application.Interface
{
    public interface IClienteApplication : IRepository<Cliente>
    {
        Task<IEnumerable<Cliente>> GetAsync();
        Task<IEnumerable<Cliente>> Page(int pagina, int qtdePorPagina, string ordem, string textoProcura);
    }
}
