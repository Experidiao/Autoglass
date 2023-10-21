using Autoglass.Application.DTO;
using Autoglass.Application.Interface;
using Autoglass.Application.Services;
using Autoglass.Domain.Interfaces;
using Autoglass.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Autoglass.Services.Api.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IUserApplication _userApplication;
        private readonly IMapper _mapper;
        public AccountController(IUserApplication userApplication, IMapper mapper)
        {
            _mapper = mapper;
            _userApplication = userApplication;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {
            var token = await _userApplication.TokenGenerate(userDTO.UserName,userDTO.Password);
            if (string.IsNullOrEmpty(token))
                return BadRequest("Usuário ou senha incorreto.");
            else
                return Ok(new { token = token });
        }
    }
}
