using ProjetoLoja.Repositorio.Interfaces;
using ProjetoLoja.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ProjetoLoja.Repositorio
{
    public class ProdutoRepositorio : BaseRepositorio, IProdutoRepositorio
    {

        public ProdutoRepositorio(ProjetoLojaContexto contexto): base(contexto)
        {
            
        }

        public async Task<int> Salvar(Produto produto)
        {
            _contexto.Produtos.Add(produto);
            await _contexto.SaveChangesAsync();
            return produto.Id;
        }

        public async Task AtualizarProduto(Produto produto)
        {
            _contexto.Produtos.Update(produto);
            await _contexto.SaveChangesAsync();
        }

        public async Task ExcluirProduto(int id)
        {
            var produto = (await ObterProdutoPorId(id)).FirstOrDefault();
            if (produto != null)
            {
                _contexto.Produtos.Remove(produto);
                await _contexto.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Produto>> ObterProdutoPorId(int id)
        {
            return await _contexto.Produtos.Where(p => p.Id == id).ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterTodosProdutos()
        {
            return await _contexto.Produtos.ToListAsync();
        }
    }
}