using Autoglass.Application.Interface;
using Autoglass.Domain.Interfaces;
using Autoglass.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Application.Services
{
    public class ClienteXProdutoApplication : IClienteXProdutoApplication
    {
        private readonly IClienteXProdutoRepository _clienteXprodutoRepository;
        public ClienteXProdutoApplication(IClienteXProdutoRepository clienteXprodutoRepository)
        {
            _clienteXprodutoRepository = clienteXprodutoRepository;
        }

        public async Task<IEnumerable<ClienteXProduto>> GetAll()
        {
            return await _clienteXprodutoRepository.GetAll();
        }

        public async Task<ClienteXProduto> GetById(int id)
        {
            return await _clienteXprodutoRepository.GetById(id);
        }

        public async Task<int> Create(ClienteXProduto clienteXproduto)
        {
                return await _clienteXprodutoRepository.Create(clienteXproduto);
        }

        public async Task<bool> Update(ClienteXProduto clienteXproduto)
        {
                return await _clienteXprodutoRepository.Update(clienteXproduto);
        }

        public async Task<bool> Delete(int Id)
        {
            ClienteXProduto xClienteXProduto = await GetById(Id);
            return await _clienteXprodutoRepository.Update(xClienteXProduto);
        }

        public async Task<IEnumerable<ClienteXProduto>> ObterProdutoDoCliente(int idCliente)
        {
            return await _clienteXprodutoRepository.ObterProdutoDoCliente(idCliente);
        }

        public async Task<IEnumerable<ClienteXProduto>> ObterProdutoDoClienteEntity(int idCliente)
        {
            return await _clienteXprodutoRepository.ObterProdutoDoClienteEntity(idCliente);
        }
        public async Task<IEnumerable<ClienteXProduto>> Page(int pagina, int qtdePorPagina, string ordem, string textoProcura)
        {
            return await _clienteXprodutoRepository.Page(pagina,qtdePorPagina,ordem,textoProcura);
        }
    }
}
