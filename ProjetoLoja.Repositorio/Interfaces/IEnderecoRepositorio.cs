using ProjetoLoja.Dominio.Entidades;

namespace ProjetoLoja.Repositorio.Interfaces
{
    public interface IEnderecoRepositorio
    {
        Task<IEnumerable<Enderecos?>> ObterTodosEnderecos();
        Task<Enderecos?> ObterEnderecoPorId(int id);
        Task<int> Salvar(Enderecos endereco);
        Task AtualizarEndereco(Enderecos endereco);
        Task ExcluirEndereco(int id);
    }
}