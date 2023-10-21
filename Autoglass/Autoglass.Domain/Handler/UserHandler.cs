using Autoglass.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Domain.Handler
{
    public class UserHandler
    {
        private readonly IConfiguration _configuration;
        public UserHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken()
        {
            // outra maneira de pegar as configuraçaoes.
           // var chave = _configuration["AppSettings:Secret"];

            var appSettingsSection = _configuration.GetSection("AppSettings");
            var keySetting = appSettingsSection.GetSection("Secret").Value;


            // handler é o manipulador 
            var handler = new JwtSecurityTokenHandler();

            // converter para um array de bytes, ou UTF8 dependendo do tipo de formataçao
            var key = Encoding.ASCII.GetBytes(keySetting);

            // assinar o token (chave secreta + tipo de assinatura)
            // Primeiro parametro precisa passar uma chave simetrica
            // Segundo parametro uma constante 

            var credential = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);


            SecurityTokenDescriptor tokenDescriptor = new()
            {
                //  Subject = GenerateClaims(user),
                SigningCredentials = credential,
                Expires = DateTime.UtcNow.AddDays(2)
            };

            // gerar o token 
            var token = handler.CreateToken(tokenDescriptor);

            // gerar uma string do token

            var strTokenString = handler.WriteToken(token);

            return strTokenString;
        }

    }
}
