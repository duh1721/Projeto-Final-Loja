using ProjetoLoja.Dominio.Entidades;
using ProjetoLoja.Aplicacao.Interfaces;
using ProjetoLoja.Repositorio.Interfaces;

namespace ProjetoLoja.Aplicacao
{
    public class TipoProdutoAplicacao : ITipoProdutoAplicacao
    {
        readonly ITipoProdutoRepositorio _tipoProdutoRepositorio;

        public TipoProdutoAplicacao(ITipoProdutoRepositorio tipoProdutoRepositorio)
        {
            _tipoProdutoRepositorio = tipoProdutoRepositorio;
        }

        public async Task<int> AdicionarTipoProduto(TipoProduto tipoProduto)
        {
            if (tipoProduto == null)
                throw new Exception("Tipo de produto não pode ser nulo");

            return await _tipoProdutoRepositorio.Salvar(tipoProduto);
        }

        public async Task AtualizarTipoProduto(TipoProduto tipoProduto)
        {
            var tipoProdutoExistente = await _tipoProdutoRepositorio.ObterTipoProdutoPorId(tipoProduto.Id);

            if (tipoProdutoExistente == null)
                throw new Exception("Tipo de produto não encontrado");

            tipoProdutoExistente.Nome = tipoProduto.Nome;
            tipoProdutoExistente.Ativo = tipoProduto.Ativo;

            await _tipoProdutoRepositorio.AtualizarTipoProduto(tipoProdutoExistente);
        }

        public async Task ExcluirTipoProduto(int id)
        {
            var tipoProdutoExistente = await _tipoProdutoRepositorio.ObterTipoProdutoPorId(id);
            if (tipoProdutoExistente == null)
                throw new Exception("Tipo de produto não encontrado");

            tipoProdutoExistente.Deletar();
            await _tipoProdutoRepositorio.AtualizarTipoProduto(tipoProdutoExistente);
        }

        public async Task<TipoProduto?> ObterTipoProdutoPorId(int id)
        {
            var tipoProdutoExistente = await _tipoProdutoRepositorio.ObterTipoProdutoPorId(id);
            if (tipoProdutoExistente == null)
                throw new Exception("Tipo de produto não encontrado");

            return tipoProdutoExistente;
        }

        public async Task<IEnumerable<TipoProduto?>> ObterTodosTiposProduto()
        {
            return await _tipoProdutoRepositorio.ObterTodosTiposProduto();
        }

        public async Task AtivarTipoProduto(int id)
        {
            var tipoProdutoExistente = await _tipoProdutoRepositorio.ObterTipoProdutoPorId(id);
            if (tipoProdutoExistente == null)
                throw new Exception("Tipo de produto não encontrado");

            tipoProdutoExistente.Restaurar();
            await _tipoProdutoRepositorio.AtualizarTipoProduto(tipoProdutoExistente);
        }
    }
}