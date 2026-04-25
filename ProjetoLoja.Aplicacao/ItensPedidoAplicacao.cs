using ProjetoLoja.Dominio.Entidades;
using ProjetoLoja.Aplicacao.Interfaces;
using ProjetoLoja.Repositorio.Interfaces;

namespace ProjetoLoja.Aplicacao
{
    public class ItensPedidoAplicacao : IItensPedidoAplicacao
    {
        readonly IItensPedidoRepositorio _itensPedidoRepositorio;

        public ItensPedidoAplicacao(IItensPedidoRepositorio itensPedidoRepositorio)
        {
            _itensPedidoRepositorio = itensPedidoRepositorio;
        }

        public async Task<int> AdicionarItensPedido(ItensPedido itensPedido)
        {
            if (itensPedido == null)
                throw new Exception("Item do pedido não pode ser nulo");

            return await _itensPedidoRepositorio.Salvar(itensPedido);
        }

        public async Task AtualizarItensPedido(ItensPedido itensPedido)
        {
            var itensPedidoExistente = await _itensPedidoRepositorio.ObterItensPedidoPorId(itensPedido.Id);

            if (itensPedidoExistente == null)
                throw new Exception("Item do pedido não encontrado");

            itensPedidoExistente.Quantidade = itensPedido.Quantidade;
            itensPedidoExistente.PrecoUnitario = itensPedido.PrecoUnitario;
            itensPedidoExistente.Ativo = itensPedido.Ativo;

            await _itensPedidoRepositorio.AtualizarItensPedido(itensPedidoExistente);
        }

        public async Task ExcluirItensPedido(int id)
        {
            var itensPedidoExistente = await _itensPedidoRepositorio.ObterItensPedidoPorId(id);
            if (itensPedidoExistente == null)
                throw new Exception("Item do pedido não encontrado");

            itensPedidoExistente.Deletar();
            await _itensPedidoRepositorio.AtualizarItensPedido(itensPedidoExistente);
        }

        public async Task<ItensPedido?> ObterItensPedidoPorId(int id)
        {
            var itensPedidoExistente = await _itensPedidoRepositorio.ObterItensPedidoPorId(id);
            if (itensPedidoExistente == null)
                throw new Exception("Item do pedido não encontrado");

            return itensPedidoExistente;
        }

        public async Task<IEnumerable<ItensPedido?>> ObterTodosItensPedido()
        {
            return await _itensPedidoRepositorio.ObterTodosItensPedido();
        }
    }
}