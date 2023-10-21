using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public decimal? Preco { get; set; }

        // Estas duas maneira sera criado campos virtual que não serão persistidos na tabela, não 
        // serão mapeados para o banco.

        //public virtual string Hora  { get { return DtValidade.Subtract(DtFabricacao).ToString(); } }
        //[NotMapped]
        //public string Dias { get { return DtValidade.Subtract(DtFabricacao).ToString(); } }

        public virtual List<ClienteXProduto> ClienteXProdutos { get; set; }
    }
}
