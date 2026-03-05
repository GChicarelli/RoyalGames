using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Royal_Games.Domains;
using Royal_Games.Exceptions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Royal_Games.DTOs.AutenticacaoDto
{
    public class GeradorTokenJwt
    {
        private readonly IConfiguration _config;

        public GeradorTokenJwt(IConfiguration config)
        {
            _config = config;
        }

        public string GerarToken(Usuario ususario)
        {
            var chave = _config["Jwt:key"];

            var issuer = _config["Jwt:issuer"];

            var audience = _config["Jwt:audience"];

            var expiraEmMinutos = int.Parse(_config["Jwt:expiraEmMinutos"]);

            var keyBytes = Encoding.UTF8.GetBytes(chave);

            if (keyBytes.Length < 32)
            {
                throw new DomainException("Jwt: Key precisa ter pelo menos 32 caracteres (256 bits)");
            }

            var securityKey = new SymmetricSecurityKey(keyBytes);

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioID.ToString()),

               new Claim(ClaimTypes.Name, usuario.Nome),

               new Claim(ClaimTypes.Email, usuario.Email)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,                                    
                audience: audience,                                
                claims: claims,                                    
                expires: DateTime.Now.AddMinutes(expiraEmMinutos), 
                signingCredentials: credentials                    
             );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
