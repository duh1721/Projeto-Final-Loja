namespace ProjetoLoja.API.Models.Login.Resposta
{
    public class LoginResposta
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Token { get; set;}
        public string Senha { get; set; }
    }
}