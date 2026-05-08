namespace ProjetoLoja.API.Models.Enderecos.Requisicao
{
    public class EnderecoCriar
    {
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public int ClienteId { get; set; }
    }
}