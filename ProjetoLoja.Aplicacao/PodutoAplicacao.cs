using ProjetoLoja.Dominio.Entidades;
using ProjetoLoja.Aplicacao.Interfaces;
using ProjetoLoja.Repositorio.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace ProjetoLoja.Aplicacao
{
    public class ProdutoAplicacao : IProdutoAplicacao
    {
        readonly IProdutoRepositorio _produtoRepositorio;

        public ProdutoAplicacao(IProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        public async Task<int> AdicionarProduto(Produto produto)
        {
            if (produto == null)
                throw new Exception("Produto nao pode ser nulo");
            ValidarInformacoesProduto(produto);

            return await _produtoRepositorio.Salvar(produto);
        }

        public async Task AtualizarProduto(Produto produto)
        {
            var produtoExistente = (await _produtoRepositorio.ObterProdutoPorId(produto.Id)).FirstOrDefault();

            if (produtoExistente == null)
                throw new Exception("Produto nao encontrado");
            ValidarInformacoesProduto(produto);

            produtoExistente.Nome = produto.Nome;
            produtoExistente.Preco = produto.Preco;
            produtoExistente.Quantidade = produto.Quantidade;
            produtoExistente.Descricao = produto.Descricao;
            produtoExistente.Ativo = produto.Ativo;

            await _produtoRepositorio.AtualizarProduto(produtoExistente);
        }

        public async Task ExcluirProduto(int id)
        {
            var produtoExistente = (await _produtoRepositorio.ObterProdutoPorId(id)).FirstOrDefault();
            if (produtoExistente == null)
                throw new Exception("Produto nao encontrado");
            produtoExistente.Deletar();

            await _produtoRepositorio.AtualizarProduto(produtoExistente);


        }

        public async Task<Produto> ObterProdutoPorId(int id)
        {
            var produtoExistente = (await _produtoRepositorio.ObterProdutoPorId(id)).FirstOrDefault();

            if (produtoExistente == null)
                throw new Exception("Produto nao encontrado");

            return produtoExistente;

        }

        public async Task<IEnumerable<Produto>> ObterTodosProdutos()
        {
            return await _produtoRepositorio.ObterTodosProdutos();
        }

        public async Task AtivarProduto(int ProdutoId)
        {
            var produtoExistente = (await _produtoRepositorio.ObterProdutoPorId(ProdutoId)).FirstOrDefault();

            if (produtoExistente == null)
                throw new Exception("Produto nao encontrado");

            produtoExistente.Restaurar();
            await _produtoRepositorio.AtualizarProduto(produtoExistente);
        }
        
        #region util
        private static void ValidarInformacoesProduto(Produto produtos)
        {

            if (string.IsNullOrEmpty(produtos.Nome))
                throw new Exception("Nome do produto nao pode ser vazio");

            if (produtos.Preco <= 0)
                throw new Exception("Preco do produto nao pode ser vazio");

            if (produtos.Quantidade <= 0)
                throw new Exception("Quantidade do produto nao pode ser vazio");
            
        }

        #endregion

    }
}