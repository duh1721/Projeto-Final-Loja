using ProjetoLoja.Dominio.Entidades;

namespace ProjetoLoja.Aplicacao.Interfaces
{
    public interface IEnderecoAplicacao
    {
        Task<IEnumerable<Enderecos?>> ObterTodosEnderecos();
        Task<Enderecos?> ObterEnderecoPorId(int id);
        Task<int> AdicionarEndereco(Enderecos endereco);
        Task AtualizarEndereco(Enderecos endereco);
        Task ExcluirEndereco(int id);
    }
}