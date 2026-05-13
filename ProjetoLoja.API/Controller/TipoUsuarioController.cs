using Microsoft.AspNetCore.Mvc;
using ProjetoLoja.Dominio.Enumeradores;

namespace ProjetoLoja.API.Controllers
{
    [ApiController]
    [Route("TiposUsuario")]
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
