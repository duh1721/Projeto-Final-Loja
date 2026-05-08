using ProjetoLoja.Dominio.Entidades;

namespace ProjetoLoja.Aplicacao.Interfaces
{
    public interface ILoginCliente
    {
        Task<Clientes> Login(string email, string senha);
    }
}