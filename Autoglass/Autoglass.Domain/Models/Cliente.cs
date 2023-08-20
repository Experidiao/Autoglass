using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Domain.Models
{
    public class Cliente
    {
        //public Cliente()
        //{
        //    ClienteXProdutos = new List<ClienteXProduto>();
        //}
        public int IdCliente {get;set;}
        public string Nome { get;set;}
        public string CpfCnpj { get;set;}
        public virtual List<ClienteXProduto> ClienteXProdutos { get; set;}

    }
}
