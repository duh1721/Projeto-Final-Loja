using Microsoft.AspNetCore.Mvc;
using ProjetoLoja.Dominio.Entidades;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using ProjetoLoja.Aplicacao;
using Microsoft.Extensions.Configuration;
using ProjetoLoja.API.Models.Login.Resposta;
using Microsoft.AspNetCore.Authorization;

namespace ProjetoLoja.API.Controller
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginCliente _loginCliente;

        public LoginController(IConfiguration configuration, ILoginCliente loginCliente)
        {
            _configuration = configuration;
            _loginCliente = loginCliente;
        }


        private string GerarToken(Clientes cliente)

        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, cliente.Email),
                    new Claim("Id", cliente.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] CriarLogin login)
        {
            try
            {
                var usuario = await _loginCliente.Login(login.Email, login.Senha);

                var token = GerarToken(usuario);

                var resposta = new LoginResposta()
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Token = token
                };
                return Ok(resposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
