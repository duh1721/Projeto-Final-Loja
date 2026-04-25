using ProjetoLoja.Dominio.Entidades;

namespace ProjetoLoja.Aplicacao.Interfaces
{
    public interface IItensPedidoAplicacao
    {
        Task<IEnumerable<ItensPedido?>> ObterTodosItensPedido();
        Task<ItensPedido?> ObterItensPedidoPorId(int id);
        Task<int> AdicionarItensPedido(ItensPedido itensPedido);
        Task AtualizarItensPedido(ItensPedido itensPedido);
        Task ExcluirItensPedido(int id);
    }
}