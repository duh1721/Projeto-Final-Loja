namespace ProjetoLoja.Dominio.Entidades
{
    public class Enderecos
    {
        public int Id { get; set; }
        public required string Rua { get; set; }
        public required string Numero { get; set; }
        public required string Bairro { get; set; }
        public required string Cidade { get; set; }
        public required string Estado { get; set; }
        public required string Cep { get; set; }
        public bool Ativo { get; set; }

        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

        public Enderecos()
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