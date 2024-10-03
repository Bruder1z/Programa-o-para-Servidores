using ApiAuth.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiAuth.Services
{
    public static class TokenService
    {
        // Classe responsável por gerar o token que vai controlar autenticação e autorização
        public static string GenerateToken(User user)
        {
            //Manipulador de token
            var tokenHandler = new JwtSecurityTokenHandler();

            // Para gerar o token precisamos da chave, mas o tokenHandler precisa que esteja em bytes
            // Transformar a chave em bytes
            var key = Encoding.ASCII.GetBytes(Setting.Secret);

            // Criar as configurações do Token, é aqui que vai conter as informações do Token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(8), // Deifine o tempo para expirar

                // Como esse token vai ser encriptado
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), //Chave
                    SecurityAlgorithms.HmacSha256Signature) // Tipo de algoritmo

            };

            // Criar o token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Retorna o token como string
            return tokenHandler.WriteToken(token);
        }
    }
}