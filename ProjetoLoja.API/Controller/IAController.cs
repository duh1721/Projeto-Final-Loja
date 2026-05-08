using Microsoft.AspNetCore.Mvc;
using ProjetoLoja.Servicos;

namespace ProjetoLoja.API.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class IAController : ControllerBase
    {
        private readonly IIAService _iaService;

        public IAController(IIAService iaService)
        {
            _iaService = iaService;
        }

        [HttpPost]
        [Route("Perguntar")]
        public async Task<ActionResult<string>> Perguntar([FromBody] string pergunta, [FromQuery] string usuarioId)
        {
            try
            {
                var resposta = await _iaService.Perguntar(pergunta, usuarioId);
                return Ok(resposta);
            }
            catch (Exception ex)
            {
                var erroCompleto = ex.InnerException?.Message ?? ex.Message;
                return BadRequest($"Erro ao obter resposta da IA: {erroCompleto}");
            }
        }
    }
}