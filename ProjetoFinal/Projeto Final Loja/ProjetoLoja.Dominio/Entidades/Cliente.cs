namespace ProjetoLoja.Dominio.Entidades
{
    public class Clientes
    {
        public int Id { get; set; }
        public  string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public int TipoUsuarioId { get; set; }
        public bool Ativo { get; set; }
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
        public ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();

        public Clientes()
        {
            Ativo = true;
        }

        public void Deletar()
        {
            Ativo = false;
        }

        public void Restaurar()
        {
            Ativo = true;
        }
    }
}