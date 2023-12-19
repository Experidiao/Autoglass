using Autoglass.Application.Interface;
using Autoglass.Domain.Models;
using Autoglass.Application.DTO;
using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;
using Microsoft.Extensions.WebEncoders.Testing;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Autoglass.Services.Api.Controllers
{
    public class ProdutoController : ApiController
    {
        private readonly IProdutoApplication _produtoApplication;
        private readonly IMapper _mapper;

        public ProdutoController(IProdutoApplication produtoApplication, IMapper mapper)
        {
            _produtoApplication = produtoApplication;
            _mapper = mapper;
        }
        // TODO: Aceitando varias rotas 
        [HttpGet()]
        [Route("Teste/{produto?}/{descricao}/{nome}")]
        [Route("Teste/{produto?}/{descricao}")]
        [Route("Teste/{produto?}")]
        public string obterTeste(int? produto, string descricao, string nome)
        {
           return "Speed";
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<Produto>> Get()
        {
            return await _produtoApplication.GetAll();
        }

        [AllowAnonymous]
        [HttpGet("GetProcurarProduto/{ordenarPor}/{valorPesquisa}/{campoPesquisa}")]
        public async Task<IEnumerable<Produto>> GetProcurarProduto(string ordenarPor="", string valorPesquisa="", string campoPesquisa="")
        {
            return await _produtoApplication.ProcurarProduto(ordenarPor, valorPesquisa, campoPesquisa);
        }

        [AllowAnonymous]
        [HttpGet("{IdProduto:int}")]
        public async Task<IActionResult> GetById(int IdProduto)
        {
            var resultado = await _produtoApplication.GetById(IdProduto);
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProdutoDTO produtoDTO)
        {
            int resposta = 0;

            try
            {
                resposta = await _produtoApplication.Create(_mapper.Map<Produto>(produtoDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "1", result = ex.Message });
            }

            return Ok(new { error = "0", result = resposta });
        }

        [AllowAnonymous]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProdutoDTO produtoDTO)
        {
            bool resposta;
            try
            {
                resposta = await _produtoApplication.Update(_mapper.Map<Produto>(produtoDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "1", result = ex.Message });

            }
            return Ok(new { erro = "0", result = resposta });
        }

        [AllowAnonymous]
        [HttpDelete("{IdProduto:int}")]
        public async Task<bool> Delete(int IdProduto)
        {
            return await _produtoApplication.Delete(IdProduto);
        }

        // desta forma os parâmetros não são obrigatórios
        [AllowAnonymous]
        [HttpGet("Page")]
        public async Task<IEnumerable<Produto>> Page(int pagina, int qtdePorPagina, string ordem, string textoProcura)
        {
            return await _produtoApplication.Page(pagina, qtdePorPagina, ordem, textoProcura);
        }

    }
}
