using Autoglass.Domain.Interfaces;
using Autoglass.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Application.Interface
{
    public interface IClienteXProdutoApplication : IRepository<ClienteXProduto>
    {
        // DAPPER
        Task<IEnumerable<ClienteXProduto>> ObterProdutoDoCliente(int idCliente);
        // entity
        Task<IEnumerable<ClienteXProduto>> ObterProdutoDoClienteEntity(int idCliente);
        Task<IEnumerable<ClienteXProduto>> Page(int pagina, int qtdePorPagina, string ordem, string textoProcura);
    }
}
