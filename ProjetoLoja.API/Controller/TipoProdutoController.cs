using Microsoft.AspNetCore.Mvc;
using ProjetoLoja.Dominio.Entidades;
using ProjetoLoja.Aplicacao.Interfaces;

namespace ProjetoLoja.API.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class TipoProdutoController : ControllerBase
    {
        private readonly ITipoProdutoAplicacao _tipoProdutoAplicacao;

        public TipoProdutoController(ITipoProdutoAplicacao tipoProdutoAplicacao)
        {
            _tipoProdutoAplicacao = tipoProdutoAplicacao;
        }

        [HttpGet]
        [Route("ObterTiposProduto")]
        public async Task<ActionResult<IEnumerable<TipoProduto?>>> ObterTodosTiposProduto()
        {
            var tiposProduto = await _tipoProdutoAplicacao.ObterTodosTiposProduto();
            return Ok(tiposProduto);
        }

        [HttpGet]
        [Route("ObterTipoProdutoPorId/{id}")]
        public async Task<ActionResult<TipoProduto?>> ObterTipoProdutoPorId(int id)
        {
            var tipoProduto = await _tipoProdutoAplicacao.ObterTipoProdutoPorId(id);
            if (tipoProduto == null)
            {
                return NotFound();
            }
            return Ok(tipoProduto);
        }

        [HttpPost]
        [Route("CriarTipoProduto")]
        public async Task<ActionResult> CriarTipoProduto([FromBody] TipoProduto tipoProduto)
        {
            try
            {
                var tipoProdutoId = await _tipoProdutoAplicacao.AdicionarTipoProduto(tipoProduto);
                return Ok($"Tipo de produto adicionado com sucesso! Id: {tipoProdutoId}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao adicionar tipo de produto: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("AtualizarTipoProduto/{id}")]
        public async Task<ActionResult> AtualizarTipoProduto(int id, TipoProduto tipoProduto)
        {
            try
            {
                tipoProduto.Id = id;
                await _tipoProdutoAplicacao.AtualizarTipoProduto(tipoProduto);
                return Ok("Tipo de produto atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar tipo de produto: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("ExcluirTipoProduto/{id}")]
        public async Task<ActionResult> ExcluirTipoProduto(int id)
        {
            try
            {
                await _tipoProdutoAplicacao.ExcluirTipoProduto(id);
                return Ok("Tipo de produto excluído com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao excluir tipo de produto: {ex.Message}");
            }
        }
    }
}