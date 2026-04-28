using ProjetoLoja.Dominio.Entidades;

namespace ProjetoLoja.Aplicacao.Interfaces
{
    public interface IPedidoAplicacao
    {
        Task<IEnumerable<Pedido?>> ObterTodosPedidos();
        Task<Pedido?> ObterPedidoPorId(int id);
        Task<int> AdicionarPedido(Pedido pedido);
        Task AtualizarPedido(Pedido pedido);
        Task ExcluirPedido(int id);
        Task AtivarPedido(int id);
    }
}