using ProjetoLoja.Dominio.Entidades;

namespace ProjetoLoja.Aplicacao.Interfaces
{
    public interface IClienteAplicacao
    {
        Task<IEnumerable<Clientes>> ObterTodosClientes();
        Task<Clientes> ObterClientePorId(int id);
        Task<int> AdicionarCliente(Clientes cliente);
        Task AtualizarCliente(Clientes cliente);
        Task ExcluirCliente(int id);
        Task AtivarCliente(int id);
    }
}