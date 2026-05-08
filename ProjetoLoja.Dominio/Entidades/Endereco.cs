namespace ProjetoLoja.Dominio.Entidades
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public bool Ativo { get; set; }
        public int ClienteId { get; set; }
        public Clientes Cliente { get; set; }

        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

        public Endereco()
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