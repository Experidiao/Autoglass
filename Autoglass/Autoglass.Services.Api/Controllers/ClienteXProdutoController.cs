using Autoglass.Application.DTO;
using Autoglass.Application.Interface;
using Autoglass.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Autoglass.Services.Api.Controllers
{
    public class ClienteXProdutoController : ApiController
    {
        private readonly IClienteXProdutoApplication _clienteXprodutoApplication;
        private readonly IMapper _mapper;
        private readonly ILogger<ClienteXProdutoController> _Logger;

        public ClienteXProdutoController(IClienteXProdutoApplication clienteXprodutoApplication, IMapper mapper, ILogger<ClienteXProdutoController> logger)
        {
            _clienteXprodutoApplication = clienteXprodutoApplication;
            _mapper = mapper;
            _Logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<ClienteXProduto>> Get()
        {
            _Logger.LogInformation("teste");
            return await _clienteXprodutoApplication.GetAll();
        }

        [AllowAnonymous]
        [HttpGet("{IdProduto:int}")]
        public async Task<IActionResult> GetById(int IdProduto)
        {
            var resultado = await _clienteXprodutoApplication.GetById(IdProduto);
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProdutoDTO produtoDTO)
        {
            int resposta = 0;

            try
            {
                resposta = await _clienteXprodutoApplication.Create(_mapper.Map<ClienteXProduto>(produtoDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "1", result = ex.Message });
            }

            return Ok(new { error = "0", result = resposta });
        }

        [AllowAnonymous]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ClienteXProdutoDTO clienteXprodutoDTO)
        {
            bool resposta;
            try
            {
                resposta = await _clienteXprodutoApplication.Update(_mapper.Map<ClienteXProduto>(clienteXprodutoDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "1", result = ex.Message });

            }
            return Ok(new { erro = "0", result = resposta });
        }

        [AllowAnonymous]
        [HttpDelete("{IdClienteXProduto:int}")]
        public async Task<bool> Delete(int IdClienteXProduto)
        {
            return await _clienteXprodutoApplication.Delete(IdClienteXProduto);
        }

        [AllowAnonymous]
        [HttpGet("ObterProdutoDoClienteDapper")]
        public  async Task<IActionResult> ObterProdutoDoCliente(int idCliente)
        {
            var itens = await _clienteXprodutoApplication.ObterProdutoDoCliente(idCliente);
            return Ok(itens);
        }

        [AllowAnonymous]
        [HttpGet("ObterProdutoDoClienteEntity")]
        public async Task<IActionResult> ObterProdutoDoClienteEntity(int idCliente)
        {
            var itens = await _clienteXprodutoApplication.ObterProdutoDoClienteEntity(idCliente);

            var itensDTO = _mapper.Map<List<ClienteXProdutoDTO>>(itens);

            foreach (var pedido in itensDTO)
            {
                var nome = pedido.produtos.Descricao; // aqui é feita a carga por Lazy Loading
            }
            return Ok(itensDTO);
        }


        // Sem decorar o método os parâmetros não são obrigatórios
        [AllowAnonymous]
        [HttpGet("Page")]
        public async Task<IActionResult> Page(int pagina, int qtdePorPagina, string ordem, string textoProcura)
        {
            var clienteXProduto = await _clienteXprodutoApplication.Page(pagina,qtdePorPagina,ordem,textoProcura);
            return Ok(clienteXProduto);
        }
    }
}

