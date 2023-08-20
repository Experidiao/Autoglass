using Autoglass.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Domain.Interfaces
{
    public interface IProdutoRepository :IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ProcurarProduto(string ordenarPor, string valorPesquisa, string campoPesquisa);
        Task<IEnumerable<Produto>> Page(int pagina, int qtdePorPagina, string ordem, string textoProcura);
    }
}
