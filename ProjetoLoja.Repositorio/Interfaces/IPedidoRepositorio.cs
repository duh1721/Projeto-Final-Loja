using ProjetoLoja.Dominio.Entidades;

namespace ProjetoLoja.Repositorio.Interfaces
{
    public interface IPedidoRepositorio
    {
        Task<IEnumerable<Pedido>> ObterTodosPedidos();
        Task<Pedido> ObterPedidoPorId(int id);
        Task<int> Salvar(Pedido pedido);
        Task AtualizarPedido(Pedido pedido);
        Task ExcluirPedido(int id);
    }
}