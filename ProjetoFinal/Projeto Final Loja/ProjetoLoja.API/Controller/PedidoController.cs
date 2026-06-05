using Microsoft.AspNetCore.Mvc;
using ProjetoLoja.Dominio.Entidades;
using ProjetoLoja.Aplicacao.Interfaces;
using ProjetoLoja.API.Models.Pedidos.Requisicao;
using ProjetoLoja.API.Models.Pedidos.Resposta;
using Microsoft.AspNetCore.Authorization;

namespace ProjetoLoja.API.Controller
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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
            try
            {
                var pedidos = await _pedidoAplicacao.ObterTodosPedidos();

                var pedidosResposta = pedidos.Select(pedido => new PedidoResposta()
                {
                    Id = pedido.Id,
                    ClienteId = pedido.ClienteId,
                    EnderecoId = pedido.EnderecoId,
                    DataPedido = pedido.DataPedido,
                    ValorTotal = pedido.ValorTotal,
                    Ativo = pedido.Ativo,
                }).ToList();
                return Ok(pedidosResposta);

            }catch (Exception ex)
            {
                return BadRequest($"Erro ao obter pedidos: {ex.Message}");
            }
            
            
    
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
        public async Task<ActionResult> CriarPedido([FromBody] PedidoCriar pedidoCriar)
        {
            try
            {
                var novoPedido = new Pedido()
                {
                    ClienteId = pedidoCriar.ClienteId,
                    EnderecoId = pedidoCriar.EnderecoId,
                    DataPedido = pedidoCriar.DataPedido,
                    ValorTotal = pedidoCriar.ValorTotal,
                    Ativo = true
                };

                var pedidoId = await _pedidoAplicacao.AdicionarPedido(novoPedido);
                return Ok($"Pedido adicionado com sucesso! Id: {pedidoId}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao adicionar pedido: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("AtualizarPedido")]
        public async Task<ActionResult> AtualizarPedido([FromBody] PedidoAtualizar pedidoAtualizar)
        {
            try
            {
                var pedidoDominio = new Pedido()
                {
                    Id = pedidoAtualizar.Id,
                    ClienteId = pedidoAtualizar.ClienteId,
                    EnderecoId = pedidoAtualizar.EnderecoId,
                    DataPedido = pedidoAtualizar.DataPedido,
                    ValorTotal = pedidoAtualizar.ValorTotal,
                    Ativo = pedidoAtualizar.Ativo
                };
                await _pedidoAplicacao.AtualizarPedido(pedidoDominio);

                return Ok($"Pedido atualizado com sucesso!\nId: {pedidoDominio.Id}\nClienteId: {pedidoDominio.ClienteId}\nEnderecoId: {pedidoDominio.EnderecoId}\nDataPedido: {pedidoDominio.DataPedido}\nValorTotal: {pedidoDominio.ValorTotal}");
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
                return Ok($"Pedido excluido com sucesso! Id: {id}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao excluir pedido: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("AtivarPedido/{id}")]
        public async Task<ActionResult> AtivarPedido(int id)
        {
            try
            {
                await _pedidoAplicacao.AtivarPedido(id);
                return Ok($"Pedido ativado com sucesso! Id: {id}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao ativar pedido: {ex.Message}");
            }
        }
    }
}