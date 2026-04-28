using Microsoft.AspNetCore.Mvc;
using ProjetoLoja.Dominio.Entidades;
using ProjetoLoja.Aplicacao.Interfaces;
using ProjetoLoja.API.Models.ItensPedido.Requisicao;
using ProjetoLoja.API.Models.ItensPedido.Resposta;

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
        public async Task<ActionResult<IEnumerable<ItensPedido>>> ObterTodosItensPedido()
        {
            try
            {
                var itensPedido = await _itensPedidoAplicacao.ObterTodosItensPedido();

                var itensPedidoResposta = itensPedido.Select(item => new ItensPedidoResposta()
                {
                    Id = item.Id,
                    PedidoId = item.PedidoId,
                    ProdutoId = item.ProdutoId,
                    Quantidade = item.Quantidade,
                    PrecoUnitario = item.PrecoUnitario
                }).ToList();
                return Ok(itensPedidoResposta);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao obter itens do pedido: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("ObterItensPedidoPorId/{id}")]
        public async Task<ActionResult<ItensPedido>> ObterItensPedidoPorId(int id)
        {
            try
            {
                var itensPedido = await _itensPedidoAplicacao.ObterItensPedidoPorId(id);
                if (itensPedido == null)
                {
                    return NotFound();
                }
                return Ok(itensPedido);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao obter item do pedido: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("CriarItensPedido")]
        public async Task<ActionResult> CriarItensPedido([FromBody] ItensPedidoCriar itensPedidoCriar)
        {
            try
            {
                var novoItem = new ItensPedido()
                {
                    PedidoId = itensPedidoCriar.PedidoId,
                    ProdutoId = itensPedidoCriar.ProdutoId,
                    Quantidade = itensPedidoCriar.Quantidade,
                    PrecoUnitario = itensPedidoCriar.PrecoUnitario
                };

                var itensPedidoId = await _itensPedidoAplicacao.AdicionarItensPedido(novoItem);
                return Ok($"Item do pedido adicionado com sucesso! Id: {itensPedidoId}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao adicionar item do pedido: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("AtualizarItensPedido")]
        public async Task<ActionResult> AtualizarItensPedido([FromBody] ItensPedidoAtualizar itensPedidoAtualizar)
        {
            try
            {
                var itemDominio = new ItensPedido()
                {
                    Id = itensPedidoAtualizar.Id,
                    PedidoId = itensPedidoAtualizar.PedidoId,
                    ProdutoId = itensPedidoAtualizar.ProdutoId,
                    Quantidade = itensPedidoAtualizar.Quantidade,
                    PrecoUnitario = itensPedidoAtualizar.PrecoUnitario
                };
                await _itensPedidoAplicacao.AtualizarItensPedido(itemDominio);

                return Ok($"Item do pedido atualizado com sucesso!\nId: {itemDominio.Id}\nPedidoId: {itemDominio.PedidoId}\nProdutoId: {itemDominio.ProdutoId}\nQuantidade: {itemDominio.Quantidade}\nPrecoUnitario: {itemDominio.PrecoUnitario}");
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
                return Ok($"Item do pedido excluido com sucesso! Id: {id}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao excluir item do pedido: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("AtivarItensPedido/{id}")]
        public async Task<ActionResult> AtivarItensPedido(int id)
        {
            try
            {
                await _itensPedidoAplicacao.AtivarItensPedido(id);
                return Ok($"Item do pedido ativado com sucesso! Id: {id}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao ativar item do pedido: {ex.Message}");
            }
        }
    }
}