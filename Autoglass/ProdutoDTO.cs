using System;
using System.Collections.Generic;
using System.Text;

namespace Autoglass.Application.DTO
{
    public class Produto
    {
        public int IdProduto { get; set; }
        public string Descricao { get; set; }
        public int Situacao { get; set; }
        public datetime DataFabricacao { get; set; }
        public dateTime DataValidade { get; set; }
        public decimal Estoque { get; set; }
        public int CodigoFornecedor { get; set; }
        public string NomeFornecedor {get;set;}
        public string CnpjFornecedor { get; set; }
    }
}
