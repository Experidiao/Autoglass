using Autoglass.Application.DTO;
using Autoglass.Application.Interface;
using Autoglass.Application.Services;
using Autoglass.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autoglass.Services.Api.Controllers
{
    public class ClienteController : ApiController
    {
        private readonly IClienteApplication _clienteApplication;
        private readonly IMapper _mapper;

        public ClienteController(IClienteApplication clienteApplication, IMapper mapper)
        {
            _clienteApplication = clienteApplication;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var cliente = await _clienteApplication.GetAsync();
            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [AllowAnonymous]
        [HttpGet("ClienteGet")]
        public async Task<IActionResult> GetAsync2()
        {
            IEnumerable<Cliente> cliente = await _clienteApplication.GetAsync();

            if (cliente == null)
                ModelState.AddModelError("Houve um erro","");
            //      return NotFound($"Não encontrado {cliente[0].Nome}");
            return NotFound();
            var clienteDTO = _mapper.Map<List<ClienteDTO>>(cliente);
            return Ok(clienteDTO);
        }

        [AllowAnonymous]
        [HttpGet("page/{pagina:int}/{qtdePorPagina:int}/{ordem}/{textoProcura?}")]
        public async Task<IActionResult> Page(int pagina,int qtdePorPagina, string ordem, string textoProcura)
        {

            var cliente = await _clienteApplication.Page(pagina, qtdePorPagina, ordem, textoProcura);
            return Ok(cliente);
        }

    }
}
