using Microsoft.AspNetCore.Mvc;
using ProjetoLoja.Dominio.Entidades;
using ProjetoLoja.Aplicacao.Interfaces;
using ProjetoLoja.API.Models.Requisicao;
using Microsoft.AspNetCore.Http.Connections;
using ProjetoLoja.API.Models.Resposta;

namespace ProjetoLoja.API.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteAplicacao _clienteAplicacao;

        public ClienteController(IClienteAplicacao clienteAplicacao)
        {
            _clienteAplicacao = clienteAplicacao;
        }

        [HttpGet]
        [Route("ObterClientes")]
        public async Task<ActionResult<IEnumerable<Clientes>>> ObterTodosClientes()
        {
            var clientes = await _clienteAplicacao.ObterTodosClientes();
            return Ok(clientes);
            
        }

        [HttpGet]
        [Route("ObterClientePorId/{id}")]
        public async Task<ActionResult<Clientes>> ObterClientePorId([FromRoute] int id)
        {
            try
            {
                var clienteDominio = await _clienteAplicacao.ObterClientePorId(id);

                var clienteResposta = new ClienteResposta()
                {
                    Id = clienteDominio.Id,
                    Nome = clienteDominio.Nome,
                    Email = clienteDominio.Email,
                    Telefone = clienteDominio.Telefone
                };
                return Ok(clienteResposta);
            }
            catch (Exception ex)
            {
                return NotFound($"Erro ao obter cliente: {ex.Message}");
            }


        }

        [HttpPost]
        [Route("CriarCliente")]
        public async Task<ActionResult> CriarCliente([FromBody] ClienteCriar cliente)
        {
            try
            {
                var clienteDominio = new Clientes()
                {
                    Nome = cliente.Nome,
                    Email = cliente.Email,
                    Telefone = cliente.Telefone
                };
                var clienteId = await _clienteAplicacao.AdicionarCliente(clienteDominio);
                return Ok($"Cliente adicionado com sucesso! Id: {clienteId}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao adicionar cliente: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("AtualizarCliente")]
        public async Task<ActionResult> AtualizarCliente([FromBody] ClienteAtualizar cliente)
        {
            try
            {
                var clienteDominio = new Clientes()
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    Email = cliente.Email,
                    Telefone = cliente.Telefone,
                };
                await _clienteAplicacao.AtualizarCliente(clienteDominio);
                return Ok($"Cliente atualizado com sucesso!\nNome: {cliente.Nome}\nEmail: {cliente.Email}\nTelefone: {cliente.Telefone}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar cliente: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("ExcluirCliente/{id}")]
        public async Task<ActionResult> ExcluirCliente(int id)
        {
            try
            {
                await _clienteAplicacao.ExcluirCliente(id);
                return Ok("Cliente excluído com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao excluir cliente: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("AtivarCliente/{id}")]
        public async Task<ActionResult> AtivarCliente(int id)
        {
            try
            {
                await _clienteAplicacao.AtivarCliente(id);
                return Ok("Cliente ativado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao ativar cliente: {ex.Message}");
            }
        }
    }
}