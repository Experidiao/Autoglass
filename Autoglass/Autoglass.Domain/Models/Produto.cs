using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Autoglass.Domain.Models
{
    public class Produto
    {
        public int IdProduto { get; set; }
        public string Descricao { get; set; }
        public DateTime DtFabricacao { get; set; }
        public DateTime DtValidade { get; set; }
        public string CodigoFornecedor { get; set; }
        public string DescricaoFornecedor { get; set; }
        public string CnpjFornecedor { get; set; }
        public int Situacao { get; set; }
    }
}
