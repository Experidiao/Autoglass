using Autoglass.Application.DTO;
using Autoglass.Application.Interface;
using Autoglass.Application.Services;
using Autoglass.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autoglass.Services.Api.Controllers
{
    public class ClienteController : ApiController
    {
        private readonly IClienteApplication _clienteApplication;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ClienteController(IClienteApplication clienteApplication, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _clienteApplication = clienteApplication;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <response code="200">All good here</response>
        /// <response code="401">Unauthorized</response>

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAsync()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

            var accessToken1 = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");


            var cliente = await _clienteApplication.GetAsync();
            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

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

        [HttpGet("page/{pagina:int}/{qtdePorPagina:int}/{ordem}/{textoProcura?}")]
        public async Task<IActionResult> Page(int pagina,int qtdePorPagina, string ordem, string textoProcura)
        {

            var cliente = await _clienteApplication.Page(pagina, qtdePorPagina, ordem, textoProcura);
            return Ok(cliente);
        }
    }

    //public async validaCliente(string : nome, string CnpjCpf)
    //{
    //    DomainExceptionValidation.
    //}
}
