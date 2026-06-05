using ProjetoLoja.Repositorio.Interfaces;
using ProjetoLoja.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ProjetoLoja.Repositorio
{
    public class ItensPedidoRepositorio : BaseRepositorio, IItensPedidoRepositorio
    {
        public ItensPedidoRepositorio(ProjetoLojaContexto contexto) : base(contexto)
        {
        }

        public async Task<int> Salvar(ItensPedido itensPedido)
        {
            _contexto.ItensPedido.Add(itensPedido);
            await _contexto.SaveChangesAsync();
            return itensPedido.Id;
        }

        public async Task AtualizarItensPedido(ItensPedido itensPedido)
        {
            _contexto.ItensPedido.Update(itensPedido);
            await _contexto.SaveChangesAsync();
        }

        public async Task ExcluirItensPedido(int id)
        {
            var itensPedido = (await ObterItensPedidoPorId(id));
            if (itensPedido != null)
            {
                _contexto.ItensPedido.Remove(itensPedido);
                await _contexto.SaveChangesAsync();
            }
        }

        public async Task<ItensPedido> ObterItensPedidoPorId(int id)
        {
            return await _contexto.ItensPedido
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<ItensPedido>> ObterTodosItensPedido()
        {
            return await _contexto.ItensPedido
                .Include(i => i.Pedido)
                .Include(i => i.Produto)
                    .ThenInclude(p => p.TipoProduto)
                .ToListAsync();
        }
    }
}