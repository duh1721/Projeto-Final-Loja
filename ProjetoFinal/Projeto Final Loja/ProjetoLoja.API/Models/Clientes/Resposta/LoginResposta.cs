namespace ProjetoLoja.API.Models.Resposta
{
    public class LoginResposta
    {
        public int LoginId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int TipoUsuarioId { get; set; }
        public string Token { get; set; }
        
    }
}