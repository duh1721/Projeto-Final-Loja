using ProjetoLoja.Dominio.Entidades;


namespace ProjetoLoja.Aplicacao.Interfaces
{
    public interface IProdutoAplicacao
    {
        Task<IEnumerable<Produtos?>> ObterTodosProdutos();
        Task<Produtos?> ObterProdutoPorId(int id);
        Task AtivarProduto(int ProdutoId);
        Task<int> AdicionarProduto(Produtos produto);
        Task AtualizarProduto(Produtos produto);
        Task ExcluirProduto(int id);
    }
}