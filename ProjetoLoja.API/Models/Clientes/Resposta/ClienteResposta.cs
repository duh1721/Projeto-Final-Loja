namespace ProjetoLoja.API.Models.Resposta
{
    public class ClienteResposta
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }
        public int TipoUsuarioId { get; set; }
        public required string Senha { get; set; }
        public bool Ativo { get; set; }
        
    }
}