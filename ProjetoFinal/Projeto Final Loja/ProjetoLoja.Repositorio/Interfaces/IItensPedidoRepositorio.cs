using ProjetoLoja.Dominio.Entidades;

namespace ProjetoLoja.Repositorio.Interfaces
{
    public interface IItensPedidoRepositorio
    {
        Task<IEnumerable<ItensPedido>> ObterTodosItensPedido();
        Task<ItensPedido> ObterItensPedidoPorId(int id);
        Task<int> Salvar(ItensPedido itensPedido);
        Task AtualizarItensPedido(ItensPedido itensPedido);
        Task ExcluirItensPedido(int id);
    }
}