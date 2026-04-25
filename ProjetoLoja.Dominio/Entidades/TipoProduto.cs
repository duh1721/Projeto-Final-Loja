namespace ProjetoLoja.Dominio.Entidades
{
    public class TipoProduto
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public bool Ativo { get; set; }

        public ICollection<Produto> Produto { get; set; } = new List<Produto>();

        public TipoProduto()
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