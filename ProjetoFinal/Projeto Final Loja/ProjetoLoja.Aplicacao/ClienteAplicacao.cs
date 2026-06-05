using ProjetoLoja.Dominio.Entidades;
using ProjetoLoja.Aplicacao.Interfaces;
using ProjetoLoja.Repositorio.Interfaces;

namespace ProjetoLoja.Aplicacao
{
    public class ClienteAplicacao : IClienteAplicacao
    {
        readonly IClienteRepositorio _clienteRepositorio;

        public ClienteAplicacao(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        public async Task<int> AdicionarCliente(Clientes cliente)
        {
            if (cliente == null)
                throw new Exception("Cliente não pode ser nulo");

            return await _clienteRepositorio.Salvar(cliente);
        }

        public async Task AtualizarCliente(Clientes cliente)
        {
            var clienteExistente = await _clienteRepositorio.ObterClientePorId(cliente.Id);

            if (clienteExistente == null)
                throw new Exception("Cliente não encontrado");

            clienteExistente.Nome = cliente.Nome;
            clienteExistente.Email = cliente.Email;
            clienteExistente.Telefone = cliente.Telefone;
            clienteExistente.Ativo = cliente.Ativo;

            await _clienteRepositorio.AtualizarCliente(clienteExistente);
        }

        public async Task ExcluirCliente(int id)
        {
            var clienteExistente = await _clienteRepositorio.ObterClientePorId(id);
            if (clienteExistente == null)
                throw new Exception("Cliente não encontrado");

            clienteExistente.Deletar();
            await _clienteRepositorio.AtualizarCliente(clienteExistente);
        }

        public async Task<Clientes> ObterClientePorId(int id)
        {
            var clienteExistente = await _clienteRepositorio.ObterClientePorId(id);
            if (clienteExistente == null)
                throw new Exception("Cliente não encontrado");

            return clienteExistente;
        }

        public async Task<IEnumerable<Clientes>> ObterTodosClientes()
        {
            return await _clienteRepositorio.ObterTodosClientes();
        }

        public async Task AtivarCliente(int id)
        {
            var clienteExistente = await _clienteRepositorio.ObterClientePorId(id);
            if (clienteExistente == null)
                throw new Exception("Cliente não encontrado");

            clienteExistente.Restaurar();
            await _clienteRepositorio.AtualizarCliente(clienteExistente);
        }
    }
}