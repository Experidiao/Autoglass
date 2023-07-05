using Autoglass.Domain.Interfaces;
using Autoglass.Domain.Models;
using Autoglass.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Autoglass.Application.Services
{
    public class ProdutoApplication : IProdutoApplication
    {
        private readonly IProdutoRepository _produtoRepository;
        public ProdutoApplication(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _produtoRepository.GetAll();
        }

        public async Task<Produto> GetById(int id)
        {
            return await _produtoRepository.GetById(id);
        }

        public async Task<List<Produto>> ProcurarProduto(string ordenarPor, string valorPesquisa, string campoPesquisa)
        {
            var listaProduto = await _produtoRepository.ProcurarProduto(ordenarPor,valorPesquisa,campoPesquisa);
            return (List<Produto>)listaProduto;
        }


        public async Task<int> Create(Produto produto)
        {
            if (!ValidationProduct(produto))
                return 0;
            else
                return await _produtoRepository.Create(produto);
        }

        public async Task<bool> Update(Produto produto)
        {
            if (!ValidationProduct(produto))
                return false;
            else
                return await _produtoRepository.Update(produto);
        }

        public async Task<bool> Delete(int Id)
        {
            Produto xProduto = await GetById(Id);
            xProduto.Situacao = 1; // 1-Inativo
            return await _produtoRepository.Update(xProduto);
        }

        public Boolean ValidationProduct(Produto produto)
        {
            int result = 0;
            if (string.IsNullOrEmpty(produto.Descricao))
            {
                result = 1;
                throw new Exception("Descrição do produto é obrigatóiro");
            }

            if (produto.DtFabricacao >= produto.DtValidade)
            {
                result = 1;
                throw new Exception("Data de Fabricação não pode ser maior ou igual a Data de Validade.");
            }

            return (result == 0);
        }
    }
}
