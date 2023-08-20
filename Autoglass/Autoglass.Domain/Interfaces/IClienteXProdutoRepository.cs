using Autoglass.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Domain.Interfaces
{
    public interface IClienteXProdutoRepository :IRepository<ClienteXProduto>
    {
        // dapper
        Task<IEnumerable<ClienteXProduto>> ObterProdutoDoCliente(int idCliente);
        // entity
        Task<IEnumerable<ClienteXProduto>> ObterProdutoDoClienteEntity(int idCliente);
        Task<IEnumerable<ClienteXProduto>> Page(int pagina, int qtdePorPagina, string ordem, string textoProcura);
    }
}
