using System;
using System.ComponentModel.DataAnnotations;

namespace AutoglassWeb.Models
{
    public class Produto
    {
        [Key]
        public int IdProduto { get; set; }

        [Required]
        public string Descricao { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DtFabricacao { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DtValidade { get; set; }
        public string CodigoFornecedor { get; set; }
        public string DescricaoFornecedor { get; set; }
        public string CnpjFornecedor { get; set; }
        public int Situacao { get; set; }
        public decimal? Preco { get; set; }  
    }
}
