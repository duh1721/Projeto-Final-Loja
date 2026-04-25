using ProjetoLoja.Dominio.Entidades;

namespace ProjetoLoja.Repositorio.Interfaces
{
    public interface IClienteRepositorio
    {
        Task<IEnumerable<Clientes?>> ObterTodosClientes();
        Task<Clientes?> ObterClientePorId(int id);
        Task<int> Salvar(Clientes cliente);
        Task AtualizarCliente(Clientes cliente);
        Task ExcluirCliente(int id);
    }
}