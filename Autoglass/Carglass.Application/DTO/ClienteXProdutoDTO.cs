using Autoglass.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Application.DTO
{


    public class ClienteXProdutoDTO
    {
        public int IdClienteXProduto { get; set; }
        public int IdCliente { get; set; }
        public int IdProduto { get; set; }
        public int QtdeItem { get; set; }
        public decimal VlTotalItem { get; set; }
       // public Cliente clientes { get;  set; }
        public virtual Produto produtos { get;  set; }


    }
}
