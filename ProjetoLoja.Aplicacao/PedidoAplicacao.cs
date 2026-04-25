using ProjetoLoja.Dominio.Entidades;
using ProjetoLoja.Aplicacao.Interfaces;
using ProjetoLoja.Repositorio.Interfaces;

namespace ProjetoLoja.Aplicacao
{
    public class PedidoAplicacao : IPedidoAplicacao
    {
        readonly IPedidoRepositorio _pedidoRepositorio;

        public PedidoAplicacao(IPedidoRepositorio pedidoRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
        }

        public async Task<int> AdicionarPedido(Pedido pedido)
        {
            if (pedido == null)
                throw new Exception("Pedido não pode ser nulo");

            return await _pedidoRepositorio.Salvar(pedido);
        }

        public async Task AtualizarPedido(Pedido pedido)
        {
            var pedidoExistente = await _pedidoRepositorio.ObterPedidoPorId(pedido.Id);

            if (pedidoExistente == null)
                throw new Exception("Pedido não encontrado");

            pedidoExistente.ValorTotal = pedido.ValorTotal;
            pedidoExistente.Ativo = pedido.Ativo;

            await _pedidoRepositorio.AtualizarPedido(pedidoExistente);
        }

        public async Task ExcluirPedido(int id)
        {
            var pedidoExistente = await _pedidoRepositorio.ObterPedidoPorId(id);
            if (pedidoExistente == null)
                throw new Exception("Pedido não encontrado");

            pedidoExistente.Deletar();
            await _pedidoRepositorio.AtualizarPedido(pedidoExistente);
        }

        public async Task<Pedido?> ObterPedidoPorId(int id)
        {
            var pedidoExistente = await _pedidoRepositorio.ObterPedidoPorId(id);
            if (pedidoExistente == null)
                throw new Exception("Pedido não encontrado");

            return pedidoExistente;
        }

        public async Task<IEnumerable<Pedido?>> ObterTodosPedidos()
        {
            return await _pedidoRepositorio.ObterTodosPedidos();
        }
    }
}