using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoglassWeb.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Text;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.Security.Policy;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AutoglassWeb.Controllers
{
    public class ProdutoController : Controller
    {
        private IConfiguration _configuration;
        private readonly string EndpointBase;
        private HttpClient client = new HttpClient();

        public ProdutoController(IConfiguration configuration)
        {
            this._configuration = configuration;
            EndpointBase = _configuration.GetSection("AppSetting").GetValue<string>("EndpointBase");
            client.BaseAddress = new Uri(EndpointBase);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        // index
        public async Task<IActionResult> Index(int? Page, string ordenarPor, string valorPesquisa = "", string campoPesquisa = "")
        {
            var xOrdenarPor = String.IsNullOrEmpty(ordenarPor) ? "Descricao" : ordenarPor;
            var xCampoPesquisar = String.IsNullOrEmpty(campoPesquisa) ? "Descricao" : campoPesquisa;
            var xValorPesquisa = String.IsNullOrEmpty(valorPesquisa) ? "%20" : valorPesquisa;

            // montar caixa com os campos para pesquisa
            IReadOnlyDictionary<string, string> xCampoPesquisa = new Dictionary<string, string>
            {
                {"Descricao","Descrição"},
                {"CodigoFornecedor","Código Fornecedor"},
                {"DescricaoFornecedor","Nome Fornecedor" }
            };

            ViewBag.campoPesquisa = new SelectList(xCampoPesquisa, "Key", "Value");



            List<Produto> produto = new List<Produto>();

            // traz todos os registros da base
            // ViewBag.valorPesquisa = string.IsNullOrEmpty(valorPesquisa) ? "" : valorPesquisa;

            int pagina = (Page ?? 1);
            int QtdPorPagina = 5;
            var rota = "Produto/GetProcurarProduto" + "/" + xOrdenarPor + "/" + xValorPesquisa + "/" + xCampoPesquisar;


            var options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNameCaseInsensitive = true
            };

        https://stackoverflow.com/questions/68246021/hosted-blazor-wasm-getfromjsonasync-the-json-value-could-not-be-converted-to-sy

            var json = await client.GetFromJsonAsync<IEnumerable<Produto>>(rota, options);

            // produto = await lerListaProdutoApi(rota);

            return View(produto.ToPagedList(pagina, QtdPorPagina));
        }

        // Incluir
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProduto,Descricao,DtFabricacao,DtValidade,CodigoFornecedor,DescricaoFornecedor,CnpjFornecedor,Situacao")] Produto produto)
        {


            if (ModelState.IsValid)
            {
                HttpResponseMessage resposta = await client.PostAsJsonAsync<Produto>("Produto/", produto);

                //using (var client = new HttpClient())
                //{
                //    //// faz a chamada do endpoint
                //    //var endPoint = EndpointBase + "Produto/";
                //    //var jsonString = JsonConvert.SerializeObject(produto);
                //    //HttpContent httpContent = new StringContent(jsonString);
                //    //httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");



                //}

                return RedirectToAction("Index");
            }
            return View(produto);
        }


        // Realizar alteração
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var produto = await lerProdutoApi(EndpointBase + "Produto/" + id);

            if (produto == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(produto);
        }

        // Enviando os dados da alteraçao
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdProduto,Descricao,DtFabricacao,DtValidade,CodigoFornecedor,DescricaoFornecedor,CnpjFornecedor,Situacao")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    // faz a chamada do endpoint
                    var endPoint = EndpointBase + "Produto";
                    var jsonString = JsonConvert.SerializeObject(produto);
                    HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    HttpResponseMessage ResultadoRequisicao = await client.PutAsync(endPoint, httpContent);
                }

                return RedirectToAction("Index");
            }
            return View(produto);
        }


        // Excluir os dados cadastrados 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                // faz a chamada do endpoint
                var endPoint = EndpointBase + "Produto/" + id;

                client.BaseAddress = new Uri(endPoint);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage ResultadoRequisicao = await client.DeleteAsync(endPoint);
            }

            return RedirectToAction("Index");
        }

        // GET: Produto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var produto = await lerProdutoApi(EndpointBase + "Produto/" + id);

            if (produto == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(produto);
        }


        // Visualizacao dos dados cadastrados
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            // executar API
            var produto = await lerProdutoApi(EndpointBase + "Produto/" + id);


            if (produto == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return View(produto);
        }

        public async Task<Produto> lerProdutoApi(string endPoint)
        {
            // Rotina para buscar os dados e, fazer a serialização.
            Produto produto = new Produto();

            using (var client = new HttpClient())
            {
                // faz a chamada do endpoint, e traz apenas um objeto
                client.BaseAddress = new Uri(endPoint);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage ResultadoRequisicao = await client.GetAsync(endPoint);
                if (ResultadoRequisicao.IsSuccessStatusCode)
                {
                    // Se tudo ocorreu bem, fazer a serialização
                    var resposta = await ResultadoRequisicao.Content.ReadAsStringAsync();
                    produto = JsonConvert.DeserializeObject<Produto>(resposta);
                }
            }
            return produto;
        }

        public async Task<List<T>> lerListaProdutoApi<T>(string rota)
        {
            string endPoint = _configuration.GetSection("AppSetting").GetValue<string>("EndpointBase");
            endPoint += rota;

            // Rotina para buscar uma lista de dados e fazer a serialização.

            using (var client = new HttpClient())
            {
                // BaseAddress - informa o endereço base Ex: '"https://localhost:44363/"'
                client.BaseAddress = new Uri(endPoint);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage ResultadoRequisicao = await client.GetAsync(endPoint);
                if (ResultadoRequisicao.IsSuccessStatusCode)
                {
                    // Se tudo ocorreu bem, fazer a serialização
                    var resposta = await ResultadoRequisicao.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<T>>(resposta);
                }
            }
            return null;
        }

    }
}
