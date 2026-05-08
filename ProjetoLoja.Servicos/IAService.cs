using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using ProjetoLoja.Aplicacao.Interfaces;
using ProjetoLoja.Repositorio.Interfaces;
using ProjetoLoja.Dominio.Entidades;
using ProjetoLoja.Servicos.Entidades;

namespace ProjetoLoja.Servicos
{
    public class IAService : IIAService
    {
        private readonly HttpClient _httpClient;
        private readonly IProdutoRepositorio _produtoRepo;
        private readonly IConfiguration _config;
        private readonly IPedidoAplicacao _pedidoAplicacao;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IEnderecoRepositorio _enderecoRepositorio;

        private static Dictionary<string, List<object>> _conversas = new();

        public IAService(
            HttpClient httpClient,
            IProdutoRepositorio produtoRepo,
            IConfiguration config,
            IPedidoAplicacao pedidoAplicacao,
            IClienteRepositorio clienteRepositorio,
            IEnderecoRepositorio enderecoRepositorio)
        {
            _httpClient = httpClient;
            _produtoRepo = produtoRepo;
            _config = config;
            _pedidoAplicacao = pedidoAplicacao;
            _clienteRepositorio = clienteRepositorio;
            _enderecoRepositorio = enderecoRepositorio;
        }

        public async Task<string> Perguntar(string pergunta, string usuarioId)
        {
            if (!_conversas.ContainsKey(usuarioId))
                _conversas[usuarioId] = new List<object>();

            var produtos = await _produtoRepo.ObterTodosProdutos();

            var listaProdutos = string.Join("\n", produtos.Select(p =>
                $"{p.Nome} - R${p.Preco} - Estoque: {p.Quantidade}"));

            if (_conversas[usuarioId].Count == 0)
            {
                _conversas[usuarioId].Add(new
                {
                    role = "system",
                    content = $"Você é uma vendedora de loja. " +
                              $"Seja simpática. " +
                              $"Peça dados faltantes. " +
                              $"Não invente produtos que não existem no banco de dados. " +
                              $"Só gere JSON quando tiver todos os dados necessários. " +
                              $"Formato JSON: {{\"acao\": \"criar_pedido\", \"produto\": \"nome\", \"quantidade\": 1, \"cliente\": \"nome\", \"rua\": \"rua\", \"numero\": \"numero\", \"cidade\": \"cidade\", \"bairro\": \"bairro\", \"cep\": \"cep\", \"estado\": \"estado\"}}. " +
                              $"Produtos disponíveis: {listaProdutos}"
                });
            }

            _conversas[usuarioId].Add(new
            {
                role = "user",
                content = pergunta
            });

            var requestBody = new
            {
                model = "llama-3.1-8b-instant",
                messages = _conversas[usuarioId]
            };

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _config["Groq:ApiKey"]);

            var response = await _httpClient.PostAsync(
                "https://api.groq.com/openai/v1/chat/completions",
                new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json")
            );

            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return $"Erro da IA: {json}";

            using var doc = JsonDocument.Parse(json);

            var resposta = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString() ?? "";

            
            _conversas[usuarioId].Add(new
            {
                role = "assistant",
                content = resposta
            });

            
            PedidoIA pedidoIA = null;

            try
            { 
                var jsonMatch = System.Text.RegularExpressions.Regex.Match(
                    resposta,
                    @"\{[\s\S]*?""acao""[\s\S]*?\}",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase
                );

                if (jsonMatch.Success)
                {
                    pedidoIA = JsonSerializer.Deserialize<PedidoIA>(
                        jsonMatch.Value,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );
                }
            }
            catch
            {
                
            }

            if (pedidoIA?.Acao != null &&
                (pedidoIA.Acao.ToLower() == "criar_pedido" || pedidoIA.Acao.ToLower() == "concluir_pedido"))
            {
                return await CriarPedidoIA(pedidoIA);
            }

            return resposta;
        }

        private async Task<string> CriarPedidoIA(PedidoIA acao)
        {
            var produtos = await _produtoRepo.ObterTodosProdutos();

            var produto = produtos
                .FirstOrDefault(p => p.Nome.ToLower().Contains(acao.Produto.ToLower()));

            if (produto == null)
                return "Não encontrei esse produto.";

            if (produto.Quantidade < acao.Quantidade)
                return "Não temos essa quantidade em estoque.";


            var cliente = await _clienteRepositorio.ObterPorNome(acao.Cliente);

            if (cliente == null)
            {
                cliente = new Clientes
                {
                    Nome = acao.Cliente,
                    Ativo = true
                };

                await _clienteRepositorio.Salvar(cliente);

                cliente = await _clienteRepositorio.ObterPorNome(acao.Cliente);
            }

            var endereco = cliente.Enderecos?.FirstOrDefault();

            if (endereco == null)
            {
                endereco = new Endereco
                {
                    Rua = acao.Rua ?? "Não informado",
                    Numero = acao.Numero ?? "S/N",
                    Cidade = acao.Cidade ?? "Não informado",
                    Bairro = acao.Bairro ?? "Não informado",
                    Cep = acao.Cep ?? "Não informado",
                    Estado = acao.Estado ?? "Não informado",
                    ClienteId = cliente.Id
                };

                await _enderecoRepositorio.Salvar(endereco);
                endereco = await _enderecoRepositorio.ObterEnderecoPorId(endereco.Id);
            }

            
            var item = new ItensPedido
            {
                ProdutoId = produto.Id,
                Quantidade = acao.Quantidade,
                PrecoUnitario = produto.Preco
            };

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                EnderecoId = endereco.Id,
                DataPedido = DateTime.Now,
                ItensPedido = new List<ItensPedido> { item },
                ValorTotal = item.Quantidade * item.PrecoUnitario,
                Ativo = true
            };

            try
            {
                var pedidoId = await _pedidoAplicacao.AdicionarPedido(pedido);


                produto.Quantidade -= acao.Quantidade;
                await _produtoRepo.AtualizarProduto(produto);

                return $"Pedido realizado com sucesso! 🛒\nNº: {pedidoId}\nCliente: {cliente.Nome}\nProduto: {produto.Nome} x{acao.Quantidade}";
            }
            catch (Exception ex)
            {
                return $"Erro ao salvar pedido: {ex.Message}";
            }
        }
    }
}