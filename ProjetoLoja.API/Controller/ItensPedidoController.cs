using Microsoft.AspNetCore.Mvc;
using ProjetoLoja.Dominio.Entidades;
using ProjetoLoja.Aplicacao.Interfaces;

namespace ProjetoLoja.API.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ItensPedidoController : ControllerBase
    {
        private readonly IItensPedidoAplicacao _itensPedidoAplicacao;

        public ItensPedidoController(IItensPedidoAplicacao itensPedidoAplicacao)
        {
            _itensPedidoAplicacao = itensPedidoAplicacao;
        }

        [HttpGet]
        [Route("ObterItensPedido")]
        public async Task<ActionResult<IEnumerable<ItensPedido?>>> ObterTodosItensPedido()
        {
            var itensPedido = await _itensPedidoAplicacao.ObterTodosItensPedido();
            return Ok(itensPedido);
        }

        [HttpGet]
        [Route("ObterItensPedidoPorId/{id}")]
        public async Task<ActionResult<ItensPedido?>> ObterItensPedidoPorId(int id)
        {
            var itensPedido = await _itensPedidoAplicacao.ObterItensPedidoPorId(id);
            if (itensPedido == null)
            {
                return NotFound();
            }
            return Ok(itensPedido);
        }

        [HttpPost]
        [Route("CriarItensPedido")]
        public async Task<ActionResult> CriarItensPedido([FromBody] ItensPedido itensPedido)
        {
            try
            {
                var itensPedidoId = await _itensPedidoAplicacao.AdicionarItensPedido(itensPedido);
                return Ok($"Item do pedido adicionado com sucesso! Id: {itensPedidoId}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao adicionar item do pedido: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("AtualizarItensPedido/{id}")]
        public async Task<ActionResult> AtualizarItensPedido(int id, ItensPedido itensPedido)
        {
            try
            {
                itensPedido.Id = id;
                await _itensPedidoAplicacao.AtualizarItensPedido(itensPedido);
                return Ok("Item do pedido atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar item do pedido: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("ExcluirItensPedido/{id}")]
        public async Task<ActionResult> ExcluirItensPedido(int id)
        {
            try
            {
                await _itensPedidoAplicacao.ExcluirItensPedido(id);
                return Ok("Item do pedido excluído com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao excluir item do pedido: {ex.Message}");
            }
        }
    }
}