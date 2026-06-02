using Microsoft.AspNetCore.Mvc;
using ProjetoLoja.Dominio.Entidades;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using ProjetoLoja.Aplicacao.Interfaces;
using ProjetoLoja.API.Models.Requisicao;
using ProjetoLoja.API.Models.Resposta;


namespace ProjetoLoja.API.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginCliente _loginCliente;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IConfiguration configuration, ILoginCliente loginCliente, ILogger<LoginController> logger)
        {
            _configuration = configuration;
            _loginCliente = loginCliente;
            _logger = logger;
        }


        private string GerarToken(Clientes cliente)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtKey = _configuration["Jwt:Key"];

            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new Exception("Jwt:Key não configurada.");
            }

            var key = Encoding.ASCII.GetBytes(jwtKey);

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
        public async Task<ActionResult> Login([FromBody] ClienteLogin login)
        {
            try
            {
                var usuario = await _loginCliente.Login(login.Email, login.Senha);

                var token = GerarToken(usuario);

                var resposta = new LoginResposta()
                {
                    LoginId = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    TipoUsuarioId = usuario.TipoUsuarioId,
                    Token = token
                };
                return Ok(resposta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante o login do usuário.");
                return Unauthorized(ex.Message);
            }
        }
    }
}
