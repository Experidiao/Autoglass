using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Domain.Models
{
    public class ClienteXProduto
    {
        public int IdClienteXProduto { get; set; }
        public int IdCliente { get; set; }
        public int IdProduto { get; set; }
        public int QtdeItem { get; set; }
        public decimal VlTotalItem { get; set; }
        public  virtual Cliente clientes { get;  set; }
        public virtual Produto produtos { get; set; }
    }
}
