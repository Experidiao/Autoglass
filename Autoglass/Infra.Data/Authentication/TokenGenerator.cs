using Autoglass.Domain.Authentication;
using Autoglass.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Authentication
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;
        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetToken(string userName, string password)
        {
            var SecretKey = _configuration.GetSection("AppSettings").GetSection("Secret").Value;
            int HoursExpire = int.Parse(_configuration.GetSection("AppSettings").GetSection("ExpiracaoHoras").Value);

            // converter para um array de bytes, ou UTF8 dependendo do tipo de formataçao
            var key = Encoding.UTF8.GetBytes(SecretKey);

            // assinar o token (chave secreta + tipo de assinatura)
            // Primeiro parametro precisa passar uma chave simetrica
            // Segundo parametro uma constante 

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            //    var ci = new ClaimsIdentity();
            //   ci.AddClaim(new Claim(ClaimTypes.Name, userName));


            SecurityTokenDescriptor tokenDescriptor = new()
            {
                //  Subject = GenerateClaims(user),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(HoursExpire)
              //  Subject = ci
            };


            // handler é o manipulador 
            var handler = new JwtSecurityTokenHandler();

            // gerar o token 
            var token = handler.CreateToken(tokenDescriptor);

            // gerar uma string do token

            var strTokenString = handler.WriteToken(token);

            return strTokenString;

            //    var claims = new List<Claim>
            //    {
            //        new Claim("UserName",userName),
            //    };
            //   var expires = DateTime.Now.AddDays(1);
            //    var key = new  SymmetricSecurityKey(Encoding.UTF8.GetBytes("teste"));
            //    var tolenDat = new JwtSecurityToken(
            //        signingCredentials: new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature),
            //        expires : expires,
            //        claims: claims
            //        );
            //    var token = new JwtSecurityTokenHandler().WriteToken(tolenDat);
            //    return token;
        }
    }
}
