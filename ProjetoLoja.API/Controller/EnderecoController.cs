using Microsoft.AspNetCore.Mvc;
using ProjetoLoja.Dominio.Entidades;
using ProjetoLoja.Aplicacao.Interfaces;

namespace ProjetoLoja.API.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoAplicacao _enderecoAplicacao;

        public EnderecoController(IEnderecoAplicacao enderecoAplicacao)
        {
            _enderecoAplicacao = enderecoAplicacao;
        }

        [HttpGet]
        [Route("ObterEnderecos")]
        public async Task<ActionResult<IEnumerable<Enderecos>>> ObterTodosEnderecos()
        {
            var enderecos = await _enderecoAplicacao.ObterTodosEnderecos();
            return Ok(enderecos);
        }

        [HttpGet]
        [Route("ObterEnderecoPorId/{id}")]
        public async Task<ActionResult<Enderecos>> ObterEnderecoPorId(int id)
        {
            var endereco = await _enderecoAplicacao.ObterEnderecoPorId(id);
            if (endereco == null)
            {
                return NotFound();
            }
            return Ok(endereco);
        }

        [HttpPost]
        [Route("CriarEndereco")]
        public async Task<ActionResult> CriarEndereco([FromBody] Enderecos endereco)
        {
            try
            {
                var enderecoId = await _enderecoAplicacao.AdicionarEndereco(endereco);
                return Ok($"Endereço adicionado com sucesso! Id: {enderecoId}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao adicionar endereço: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("AtualizarEndereco/{id}")]
        public async Task<ActionResult> AtualizarEndereco(int id, Enderecos endereco)
        {
            try
            {
                endereco.Id = id;
                await _enderecoAplicacao.AtualizarEndereco(endereco);
                return Ok("Endereço atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar endereço: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("ExcluirEndereco/{id}")]
        public async Task<ActionResult> ExcluirEndereco(int id)
        {
            try
            {
                await _enderecoAplicacao.ExcluirEndereco(id);
                return Ok("Endereço excluído com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao excluir endereço: {ex.Message}");
            }
        }
    }
}