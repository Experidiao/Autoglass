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
    public class ClienteApplication : IClienteApplication
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteApplication(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<Cliente>> GetAsync()
        {
            return await _clienteRepository.GetAll();
        }
        public async Task<IEnumerable<Cliente>> GetAll()
        {
            return await _clienteRepository.GetAll();
        }

        public async Task<Cliente> GetById(int id)
        {
            return await _clienteRepository.GetById(id);
        }

        public async Task<int> Create(Cliente cliente)
        {
                return await _clienteRepository.Create(cliente);
        }

        public async Task<bool> Update(Cliente cliente)
        {
                return await _clienteRepository.Update(cliente);
        }

        public async Task<bool> Delete(int Id)
        {
            Cliente xCliente = await GetById(Id);
            return await _clienteRepository.Update(xCliente);
        }
        public async Task<IEnumerable<Cliente>> Page(int pagina, int qtdePorPagina, string ordem, string textoProcura)
        {
            return await _clienteRepository.Page(pagina, qtdePorPagina,ordem,textoProcura);
        }
    }
}
