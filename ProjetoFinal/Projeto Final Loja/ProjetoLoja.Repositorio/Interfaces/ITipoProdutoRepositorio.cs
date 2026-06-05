using ProjetoLoja.Dominio.Entidades;

namespace ProjetoLoja.Repositorio.Interfaces
{
    public interface ITipoProdutoRepositorio
    {
        Task<IEnumerable<TipoProduto>> ObterTodosTiposProduto();
        Task<TipoProduto> ObterTipoProdutoPorId(int id);
        Task<int> Salvar(TipoProduto tipoProduto);
        Task AtualizarTipoProduto(TipoProduto tipoProduto);
        Task ExcluirTipoProduto(int id);
    }
}