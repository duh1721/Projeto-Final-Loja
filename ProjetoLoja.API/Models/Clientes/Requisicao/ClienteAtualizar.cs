namespace ProjetoLoja.API.Models.Requisicao
{
    public class ClienteAtualizar
    {
        public required int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }
        public required string Senha { get; set; }
        public int TipoUsuarioId { get; set; }
    }
}