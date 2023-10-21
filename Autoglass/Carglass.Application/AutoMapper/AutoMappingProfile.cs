using Autoglass.Application.DTO;
using Autoglass.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Application.AutoMapper
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            // mapeando classe origem para classe destino. ReverseMap indica que pode ser ao contrário também.

            CreateMap<Produto, ProdutoDTO>().ReverseMap();
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<ClienteXProduto, ClienteXProdutoDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}


