using System.ComponentModel;

namespace ProjetoLoja.Dominio.Entidades
{
    public class Produtos
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public required string Descricao { get; set; }
        public bool Ativo { get; set; }


        public Produtos()
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