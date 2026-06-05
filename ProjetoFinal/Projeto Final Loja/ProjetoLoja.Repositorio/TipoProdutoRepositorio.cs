using ProjetoLoja.Repositorio.Interfaces;
using ProjetoLoja.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ProjetoLoja.Repositorio
{
    public class TipoProdutoRepositorio : BaseRepositorio, ITipoProdutoRepositorio
    {
        public TipoProdutoRepositorio(ProjetoLojaContexto contexto) : base(contexto)
        {
        }

        public async Task<int> Salvar(TipoProduto tipoProduto)
        {
            _contexto.TiposProduto.Add(tipoProduto);
            await _contexto.SaveChangesAsync();
            return tipoProduto.Id;
        }

        public async Task AtualizarTipoProduto(TipoProduto tipoProduto)
        {
            _contexto.TiposProduto.Update(tipoProduto);
            await _contexto.SaveChangesAsync();
        }

        public async Task ExcluirTipoProduto(int id)
        {
            var tipoProduto = (await ObterTipoProdutoPorId(id));
            if (tipoProduto != null)
            {
                _contexto.TiposProduto.Remove(tipoProduto);
                await _contexto.SaveChangesAsync();
            }
        }

        public async Task<TipoProduto> ObterTipoProdutoPorId(int id)
        {
            var tipoProduto = await _contexto.TiposProduto.FirstOrDefaultAsync(t => t.Id == id);
            if (tipoProduto == null)
                throw new Exception("Tipo de produto não encontrado");

            return tipoProduto;
        }

        public async Task<IEnumerable<TipoProduto>> ObterTodosTiposProduto()
        {
            return await _contexto.TiposProduto.ToListAsync();
        }
    }
}