using System.Text.Json.Serialization;

namespace ProjetoLoja.Servicos.Entidades
{
    public class PedidoIA
    {
        [JsonPropertyName("acao")]
        public string Acao { get; set; }

        [JsonPropertyName("produto")]
        public string Produto { get; set; }

        [JsonPropertyName("quantidade")]
        public int Quantidade { get; set; }

        [JsonPropertyName("cliente")]
        public string Cliente { get; set; }

        [JsonPropertyName("rua")]
        public string Rua { get; set; }

        [JsonPropertyName("numero")]
        public string Numero { get; set; }

        [JsonPropertyName("cidade")]
        public string Cidade { get; set; }

        [JsonPropertyName("bairro")]
        public string Bairro { get; set; }

        [JsonPropertyName("cep")]
        public string Cep { get; set; }

        [JsonPropertyName("estado")]
        public string Estado { get; set; }
        
        [JsonPropertyName("observacao")]
        public string Observacao { get; set; }
    }
}