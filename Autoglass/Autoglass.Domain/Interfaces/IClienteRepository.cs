using Autoglass.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Domain.Interfaces
{
    public  interface IClienteRepository :IRepository<Cliente>
    {
        Task<IEnumerable<Cliente>> Page(int pagina, int qtdePorPagina,string ordem, string textoProcura);
    }
}
