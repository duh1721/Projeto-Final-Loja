using ProjetoLoja.Dominio.Entidades;

namespace ProjetoLoja.Aplicacao.Interfaces
{
    public interface IEnderecoAplicacao
    {
        Task<IEnumerable<Endereco>> ObterTodosEnderecos();
        Task<Endereco> ObterEnderecoPorId(int id);
        Task<int> AdicionarEndereco(Endereco endereco);
        Task AtualizarEndereco(Endereco endereco);
        Task ExcluirEndereco(int id);
    }
}