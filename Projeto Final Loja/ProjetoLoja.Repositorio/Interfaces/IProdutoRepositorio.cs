using ProjetoLoja.Dominio.Entidades;

namespace ProjetoLoja.Repositorio.Interfaces
{
    public interface IProdutoRepositorio
    {
        Task<IEnumerable<Produtos?>> ObterTodosProdutos();
        Task<IEnumerable<Produtos?>> ObterProdutoPorId(int id);
        Task<int> Salvar(Produtos produto);
        Task AtualizarProduto(Produtos produto);
        Task ExcluirProduto(int id);
    }
}