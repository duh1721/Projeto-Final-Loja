using ProjetoLoja.Dominio.Entidades;

namespace ProjetoLoja.Repositorio.Interfaces
{
    public interface IEnderecoRepositorio
    {
        Task<IEnumerable<Endereco>> ObterTodosEnderecos();
        Task<Endereco> ObterEnderecoPorId(int id);
        Task<int> Salvar(Endereco endereco);
        Task AtualizarEndereco(Endereco endereco);
        Task ExcluirEndereco(int id);
    }
}