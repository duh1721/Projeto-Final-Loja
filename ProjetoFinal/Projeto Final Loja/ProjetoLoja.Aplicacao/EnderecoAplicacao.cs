using ProjetoLoja.Dominio.Entidades;
using ProjetoLoja.Aplicacao.Interfaces;
using ProjetoLoja.Repositorio.Interfaces;

namespace ProjetoLoja.Aplicacao
{
    public class EnderecoAplicacao : IEnderecoAplicacao
    {
        readonly IEnderecoRepositorio _enderecoRepositorio;

        public EnderecoAplicacao(IEnderecoRepositorio enderecoRepositorio)
        {
            _enderecoRepositorio = enderecoRepositorio;
        }

        public async Task<int> AdicionarEndereco(Endereco endereco)
        {
            if (endereco == null)
                throw new Exception("Endereço não pode ser nulo");

            return await _enderecoRepositorio.Salvar(endereco);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            var enderecoExistente = await _enderecoRepositorio.ObterEnderecoPorId(endereco.Id);

            if (enderecoExistente == null)
                throw new Exception("Endereço não encontrado");

            enderecoExistente.Rua = endereco.Rua;
            enderecoExistente.Numero = endereco.Numero;
            enderecoExistente.Bairro = endereco.Bairro;
            enderecoExistente.Cidade = endereco.Cidade;
            enderecoExistente.Estado = endereco.Estado;
            enderecoExistente.Cep = endereco.Cep;
            enderecoExistente.Ativo = endereco.Ativo;

            await _enderecoRepositorio.AtualizarEndereco(enderecoExistente);
        }

        public async Task ExcluirEndereco(int id)
        {
            var enderecoExistente = await _enderecoRepositorio.ObterEnderecoPorId(id);
            if (enderecoExistente == null)
                throw new Exception("Endereço não encontrado");

            enderecoExistente.Deletar();
            await _enderecoRepositorio.AtualizarEndereco(enderecoExistente);
        }

        public async Task<Endereco> ObterEnderecoPorId(int id)
        {
            var enderecoExistente = await _enderecoRepositorio.ObterEnderecoPorId(id);
            if (enderecoExistente == null)
                throw new Exception("Endereço não encontrado");

            return enderecoExistente;
        }

        public async Task<IEnumerable<Endereco>> ObterTodosEnderecos()
        {
            return await _enderecoRepositorio.ObterTodosEnderecos();
        }
    }
}