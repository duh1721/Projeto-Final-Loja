using ProjetoLoja.Dominio.Entidades;


namespace ProjetoLoja.Aplicacao.Interfaces
{
    public interface IProdutoAplicacao
    {
        Task<IEnumerable<Produto>> ObterTodosProdutos();
        Task<Produto> ObterProdutoPorId(int id);
        Task AtivarProduto(int ProdutoId);
        Task<int> AdicionarProduto(Produto produto);
        Task AtualizarProduto(Produto produto);
        Task ExcluirProduto(int id);
    }
}