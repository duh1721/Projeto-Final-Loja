using ProjetoLoja.Dominio.Entidades;
using ProjetoLoja.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ProjetoLoja.API.Models.Requisicao;
using ProjetoLoja.API.Models.Resposta;
using ProjetoLoja.Aplicacao.Interfaces;


namespace ProjetoLoja.API.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoAplicacao _produtoAplicacao;

        public ProdutoController(IProdutoAplicacao produtoAplicacao)
        {
            _produtoAplicacao = produtoAplicacao;
        }

        [HttpGet]
        [Route("ObterProdutos")]
        public async Task<ActionResult<IEnumerable<Produtos?>>> ObterTodosProdutos()
        {
            var produtos = await _produtoAplicacao.ObterTodosProdutos();
            return Ok(produtos);
        }

        [HttpGet]
        [Route("ObterProdutoPorId/{id}")]
        public async Task<ActionResult<Produtos?>> ObterProdutoPorId(int id)
        {
            var produto = await _produtoAplicacao.ObterProdutoPorId(id);
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        [HttpPost]
        [Route("CriarProduto")]
        public async Task<ActionResult> CriarProduto([FromBody] ProdutoCriar produtoCriar)
        {
            try
            {
                var produtoDominio = new Produtos()
                {
                    Nome = produtoCriar.Nome,
                    Preco = produtoCriar.Preco,
                    Quantidade = produtoCriar.Quantidade,
                    Descricao = produtoCriar.Descricao,
                };

                var produtoId = await _produtoAplicacao.AdicionarProduto(produtoDominio);

                return Ok($"Produto adicionado com sucesso! Id: {produtoId}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao adicionar produto: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("AtualizarProduto/{id}")]
        public async Task<ActionResult> AtualizarProduto(int id, Produtos produto)
        {
            try
            {
                var produtoDominio = new Produtos()
                {
                    Id = id,
                    Nome = produto.Nome,
                    Preco = produto.Preco,
                    Quantidade = produto.Quantidade,
                    Descricao = produto.Descricao,
                    Ativo = produto.Ativo
                };
                await _produtoAplicacao.AtualizarProduto(produtoDominio);

                return Ok($"Produto atualizado com sucesso!\nId: {id}\nNome: {produto.Nome}\nPreco: {produto.Preco}\nQuantidade: {produto.Quantidade}\nDescricao: {produto.Descricao}\nAtivo: {produto.Ativo}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar produto: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("ExcluirProduto/{id}")]
        public async Task<ActionResult> DeleteProduto(int id)
        {
            try
            {
                await _produtoAplicacao.ExcluirProduto(id);

                return Ok($"Produto excluido com sucesso! Id: {id}");

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao excluir produto: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("AtivarProduto/{id}")]
        public async Task<ActionResult> AtivarProduto(int id)
        {
            try
            {
                await _produtoAplicacao.AtivarProduto(id);

                return Ok($"Produto ativado com sucesso! Id: {id}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao ativar produto: {ex.Message}");
            }
        }
    }
}