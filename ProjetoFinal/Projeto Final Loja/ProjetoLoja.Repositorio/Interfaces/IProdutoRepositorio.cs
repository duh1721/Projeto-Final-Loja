using ProjetoLoja.Dominio.Entidades;

namespace ProjetoLoja.Repositorio.Interfaces
{
    public interface IProdutoRepositorio
    {
        Task<IEnumerable<Produto>> ObterTodosProdutos();
        Task<IEnumerable<Produto>> ObterProdutoPorId(int id);
        Task<int> Salvar(Produto produto);
        Task AtualizarProduto(Produto produto);
        Task ExcluirProduto(int id);
    }
}