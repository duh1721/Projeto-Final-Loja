namespace ProjetoLoja.Dominio.Entidades
{
    public class Clientes
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }
        public bool Ativo { get; set; }

        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

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