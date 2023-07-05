using Autoglass.Domain.Interfaces;
using Autoglass.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Application.Interface
{
    public interface IProdutoApplication : IRepository<Produto>
    {
        Task<List<Produto>> ProcurarProduto(string ordenarPor, string valorPesquisa, string campoPesquisa);
    }
}
