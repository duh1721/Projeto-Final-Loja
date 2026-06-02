using Microsoft.AspNetCore.Mvc;
using ProjetoLoja.Dominio.Enumeradores;
using Microsoft.AspNetCore.Authorization;
namespace ProjetoLoja.API.Controllers
{
    [ApiController]
    [Route("TiposUsuario")]
    [Authorize]
    public class TiposUsuarioController : ControllerBase
    {
        [HttpGet("ListarTiposUsuario")]
        public IActionResult ListarTiposUsuario()
        {
            try
            {
                var lista = Enum.GetValues(typeof(TipoUsuario))
                    .Cast<TipoUsuario>()
                    .Select(t => new
                    {
                        id = (int)t,
                        nome = t.ToString()
                    })
                    .ToList();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
