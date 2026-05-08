using ProjetoLoja.Dominio.Entidades;

namespace ProjetoLoja.Aplicacao
{
    public interface ILoginCliente
    {
        Task<Clientes> Login(string email, string senha);
    }
}