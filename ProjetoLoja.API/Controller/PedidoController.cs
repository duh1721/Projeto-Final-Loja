using Microsoft.AspNetCore.Mvc;
using ProjetoLoja.Dominio.Entidades;
using ProjetoLoja.Aplicacao.Interfaces;

namespace ProjetoLoja.API.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoAplicacao _pedidoAplicacao;

        public PedidoController(IPedidoAplicacao pedidoAplicacao)
        {
            _pedidoAplicacao = pedidoAplicacao;
        }

        [HttpGet]
        [Route("ObterPedidos")]
        public async Task<ActionResult<IEnumerable<Pedido>>> ObterTodosPedidos()
        {
            var pedidos = await _pedidoAplicacao.ObterTodosPedidos();
            return Ok(pedidos);
        }

        [HttpGet]
        [Route("ObterPedidoPorId/{id}")]
        public async Task<ActionResult<Pedido>> ObterPedidoPorId(int id)
        {
            var pedido = await _pedidoAplicacao.ObterPedidoPorId(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        [HttpPost]
        [Route("CriarPedido")]
        public async Task<ActionResult> CriarPedido([FromBody] Pedido pedido)
        {
            try
            {
                var pedidoId = await _pedidoAplicacao.AdicionarPedido(pedido);
                return Ok($"Pedido adicionado com sucesso! Id: {pedidoId}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao adicionar pedido: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("AtualizarPedido/{id}")]
        public async Task<ActionResult> AtualizarPedido(int id, Pedido pedido)
        {
            try
            {
                pedido.Id = id;
                await _pedidoAplicacao.AtualizarPedido(pedido);
                return Ok("Pedido atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar pedido: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("ExcluirPedido/{id}")]
        public async Task<ActionResult> ExcluirPedido(int id)
        {
            try
            {
                await _pedidoAplicacao.ExcluirPedido(id);
                return Ok("Pedido excluído com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao excluir pedido: {ex.Message}");
            }
        }
    }
}