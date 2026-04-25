using ProjetoLoja.Repositorio.Interfaces;
using ProjetoLoja.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ProjetoLoja.Repositorio
{
    public class ClienteRepositorio : BaseRepositorio, IClienteRepositorio
    {
        public ClienteRepositorio(ProjetoLojaContexto contexto) : base(contexto)
        {
        }

        public async Task<int> Salvar(Clientes cliente)
        {
            _contexto.Clientes.Add(cliente);
            await _contexto.SaveChangesAsync();
            return cliente.Id;
        }

        public async Task AtualizarCliente(Clientes cliente)
        {
            _contexto.Clientes.Update(cliente);
            await _contexto.SaveChangesAsync();
        }

        public async Task ExcluirCliente(int id)
        {
            var cliente = (await ObterClientePorId(id));
            if (cliente != null)
            {
                _contexto.Clientes.Remove(cliente);
                await _contexto.SaveChangesAsync();
            }
        }

        public async Task<Clientes?> ObterClientePorId(int id)
        {
            return await _contexto.Clientes.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Clientes?>> ObterTodosClientes()
        {
            return await _contexto.Clientes.ToListAsync();
        }
    }
}