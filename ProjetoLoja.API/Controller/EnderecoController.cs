using Microsoft.AspNetCore.Mvc;
using ProjetoLoja.Dominio.Entidades;
using ProjetoLoja.Aplicacao.Interfaces;
using ProjetoLoja.API.Models.Enderecos.Requisicao;
using ProjetoLoja.API.Models.Enderecos.Resposta;


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
            
            var enderecoResposta = new EnderecoResposta()
            {
                Id = endereco.Id,
                Rua = endereco.Rua,
                Numero = endereco.Numero,
                Bairro = endereco.Bairro,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                Cep = endereco.Cep
            };
            return Ok(enderecoResposta);
        }

        [HttpPost]
        [Route("CriarEndereco")]
        public async Task<ActionResult> CriarEndereco([FromBody] EnderecoCriar endereco)
        {
            try
            {
                var novoEndereco = new Enderecos()
                {
                    Rua = endereco.Rua,
                    Numero = endereco.Numero,
                    Bairro = endereco.Bairro,
                    Cidade = endereco.Cidade,
                    Estado = endereco.Estado,
                    Cep = endereco.Cep
                };
                await _enderecoAplicacao.AdicionarEndereco(novoEndereco);
                return Ok("Endereço criado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao adicionar endereço: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("AtualizarEndereco/{id}")]
        public async Task<ActionResult> AtualizarEndereco(int id, EnderecoAtualizar endereco)
        {
            try
            {
                var enderecoExistente = await _enderecoAplicacao.ObterEnderecoPorId(id);
                if (enderecoExistente == null)
                {
                    return NotFound();
                }

                enderecoExistente.Rua = endereco.Rua;
                enderecoExistente.Numero = endereco.Numero;
                enderecoExistente.Bairro = endereco.Bairro;
                enderecoExistente.Cidade = endereco.Cidade;
                enderecoExistente.Estado = endereco.Estado;
                enderecoExistente.Cep = endereco.Cep;

                await _enderecoAplicacao.AtualizarEndereco(enderecoExistente);
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