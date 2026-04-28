using ProjetoLoja.Dominio.Entidades;

namespace ProjetoLoja.Aplicacao.Interfaces
{
    public interface ITipoProdutoAplicacao
    {
        Task<IEnumerable<TipoProduto?>> ObterTodosTiposProduto();
        Task<TipoProduto?> ObterTipoProdutoPorId(int id);
        Task<int> AdicionarTipoProduto(TipoProduto tipoProduto);
        Task AtualizarTipoProduto(TipoProduto tipoProduto);
        Task ExcluirTipoProduto(int id);
        Task AtivarTipoProduto(int id);
    }
}