using ProjetoLoja.Repositorio.Interfaces;
using ProjetoLoja.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ProjetoLoja.Repositorio
{
    public class EnderecoRepositorio : BaseRepositorio, IEnderecoRepositorio
    {
        public EnderecoRepositorio(ProjetoLojaContexto contexto) : base(contexto)
        {
        }

        public async Task<int> Salvar(Enderecos endereco)
        {
            _contexto.Enderecos.Add(endereco);
            await _contexto.SaveChangesAsync();
            return endereco.Id;
        }

        public async Task AtualizarEndereco(Enderecos endereco)
        {
            _contexto.Enderecos.Update(endereco);
            await _contexto.SaveChangesAsync();
        }

        public async Task ExcluirEndereco(int id)
        {
            var endereco = (await ObterEnderecoPorId(id));
            if (endereco != null)
            {
                _contexto.Enderecos.Remove(endereco);
                await _contexto.SaveChangesAsync();
            }
        }

        public async Task<Enderecos?> ObterEnderecoPorId(int id)
        {
            return await _contexto.Enderecos.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Enderecos?>> ObterTodosEnderecos()
        {
            return await _contexto.Enderecos.ToListAsync();
        }
    }
}