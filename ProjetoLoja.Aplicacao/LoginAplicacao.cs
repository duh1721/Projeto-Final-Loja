using ProjetoLoja.Dominio.Entidades;
using ProjetoLoja.Repositorio.Interfaces;
using ProjetoLoja.Aplicacao.Interfaces;
using BCrypt.Net;
namespace ProjetoLoja.Aplicacao
{
    public class LoginAplicacao : ILoginCliente
    {
        private readonly IClienteRepositorio clienteRepositorio;

        public LoginAplicacao(IClienteRepositorio clienteRepositorio)
        {
            this.clienteRepositorio = clienteRepositorio;
        }

        public async Task<Clientes> Login(string email, string senha)
        {
            if (string.IsNullOrEmpty(email))
                throw new Exception("Email não pode ser vazio");

            if (string.IsNullOrEmpty(senha))
                throw new Exception("Senha não pode ser vazia");


            var usuarioExistente = (await clienteRepositorio.ObterPorEmail(email)).FirstOrDefault();

            if (usuarioExistente == null)
                throw new Exception("Usuário não encontrado");

            if (!BCrypt.Net.BCrypt.Verify(senha, usuarioExistente.Senha))
                throw new Exception("Senha inválida");
            
            if(!usuarioExistente.Ativo)
                throw new Exception("Usuário inativo, Entre em contato com o suporte");


            return usuarioExistente;
        }
    }
}