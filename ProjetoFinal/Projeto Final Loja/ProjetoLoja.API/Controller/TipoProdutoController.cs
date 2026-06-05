using Microsoft.AspNetCore.Mvc;
using ProjetoLoja.Dominio.Entidades;
using ProjetoLoja.Aplicacao.Interfaces;
using ProjetoLoja.API.Models.TipoProduto.Requisicao;
using ProjetoLoja.API.Models.TipoProduto.Resposta;
using Microsoft.AspNetCore.Authorization;

namespace ProjetoLoja.API.Controller
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TipoProdutoController : ControllerBase
    {
        private readonly ITipoProdutoAplicacao _tipoProdutoAplicacao;

        public TipoProdutoController(ITipoProdutoAplicacao tipoProdutoAplicacao)
        {
            _tipoProdutoAplicacao = tipoProdutoAplicacao;
        }

        [HttpGet]
        [Route("ObterTiposProduto")]
        public async Task<ActionResult<IEnumerable<TipoProduto>>> ObterTodosTiposProduto()
        {
            try
            {
                var tiposProduto = await _tipoProdutoAplicacao.ObterTodosTiposProduto();

                var tiposProdutoResposta = tiposProduto.Select(tipo => new TipoProdutoResposta()
                {
                    Id = tipo.Id,
                    Nome = tipo.Nome,
                    Ativo = tipo.Ativo
                }).ToList();
                return Ok(tiposProdutoResposta);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao obter tipos de produto: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("ObterTipoProdutoPorId/{id}")]
        public async Task<ActionResult<TipoProduto>> ObterTipoProdutoPorId(int id)
        {
            try
            {
                var tipoProduto = await _tipoProdutoAplicacao.ObterTipoProdutoPorId(id);
                return Ok(tipoProduto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao obter tipo de produto: {ex.Message}");
            }
            
        }

        [HttpPost]
        [Route("CriarTipoProduto")]
        public async Task<ActionResult> CriarTipoProduto([FromBody] TipoProdutoCriar tipoProdutoCriar)
        {
            try
            {
                var novoTipo = new TipoProduto()
                {
                    Nome = tipoProdutoCriar.Nome
                };

                var tipoProdutoId = await _tipoProdutoAplicacao.AdicionarTipoProduto(novoTipo);
                return Ok($"Tipo de produto adicionado com sucesso! Id: {tipoProdutoId}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao adicionar tipo de produto: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("AtualizarTipoProduto")]
        public async Task<ActionResult> AtualizarTipoProduto([FromBody] TipoProdutoAtualizar tipoProdutoAtualizar)
        {
            try
            {
                var tipoDominio = new TipoProduto()
                {
                    Id = tipoProdutoAtualizar.Id,
                    Nome = tipoProdutoAtualizar.Nome
                };
                await _tipoProdutoAplicacao.AtualizarTipoProduto(tipoDominio);

                return Ok($"Tipo de produto atualizado com sucesso!\nId: {tipoDominio.Id}\nNome: {tipoDominio.Nome}");
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
                return Ok($"Tipo de produto excluido com sucesso! Id: {id}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao excluir tipo de produto: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("AtivarTipoProduto/{id}")]
        public async Task<ActionResult> AtivarTipoProduto(int id)
        {
            try
            {
                await _tipoProdutoAplicacao.AtivarTipoProduto(id);
                return Ok($"Tipo de produto ativado com sucesso! Id: {id}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao ativar tipo de produto: {ex.Message}");
            }
        }
    }
}