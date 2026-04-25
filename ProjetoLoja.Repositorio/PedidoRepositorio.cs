using ProjetoLoja.Repositorio.Interfaces;
using ProjetoLoja.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ProjetoLoja.Repositorio
{
    public class PedidoRepositorio : BaseRepositorio, IPedidoRepositorio
    {
        public PedidoRepositorio(ProjetoLojaContexto contexto) : base(contexto)
        {
        }

        public async Task<int> Salvar(Pedido pedido)
        {
            _contexto.Pedidos.Add(pedido);
            await _contexto.SaveChangesAsync();
            return pedido.Id;
        }

        public async Task AtualizarPedido(Pedido pedido)
        {
            _contexto.Pedidos.Update(pedido);
            await _contexto.SaveChangesAsync();
        }

        public async Task ExcluirPedido(int id)
        {
            var pedido = (await ObterPedidoPorId(id));
            if (pedido != null)
            {
                _contexto.Pedidos.Remove(pedido);
                await _contexto.SaveChangesAsync();
            }
        }

        public async Task<Pedido?> ObterPedidoPorId(int id)
        {
            return await _contexto.Pedidos
                .Include(p => p.ItensPedido)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Pedido?>> ObterTodosPedidos()
        {
            return await _contexto.Pedidos
                .Include(p => p.ItensPedido)
                .ToListAsync();
        }
    }
}