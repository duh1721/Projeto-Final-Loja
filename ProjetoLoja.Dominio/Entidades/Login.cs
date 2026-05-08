namespace ProjetoLoja.Dominio.Entidades
{
    public class Login
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Clientes Cliente { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }

        public Login()
        {
            Ativo = true;
        }


    }
}