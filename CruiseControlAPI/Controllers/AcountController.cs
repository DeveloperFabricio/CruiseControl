using CruiseControl.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CruiseControlAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcountController : ControllerBase
    {
        [HttpPost]

        public IActionResult Login([FromBody] LoginSystem login)
        {
            if (login.Login == "admin" && login.Password == "admin")
            {
                var token = GerarTokenJwt();
                return Ok(new { token });
            }
            return BadRequest("Credenciais inválidas. Por favor, verifique seu nome de usuário e senha!");
        }

        private string GerarTokenJwt()
        {
            string chaveSecreta = "eeccd01b-e77c-44f9-bb9f-a519b3105dce";

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
            var credencial = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("login", "admin"),
                new Claim("nome", "System Administrator")
            };

            var token = new JwtSecurityToken(
                issuer: "CruiseControl",
                audience: "CruiseControl",
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credencial
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
