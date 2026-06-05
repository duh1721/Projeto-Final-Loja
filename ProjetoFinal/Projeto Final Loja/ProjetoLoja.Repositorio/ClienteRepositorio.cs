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

        public async Task<Clientes> ObterClientePorId(int id)
        {
            var cliente = await _contexto.Clientes.Include(c => c.Enderecos).FirstOrDefaultAsync(c => c.Id == id);
            if (cliente == null)
                throw new InvalidOperationException("Cliente não encontrado.");
            return cliente;
        }
        
        public async Task<Clientes> ObterPorNome(string nome)
        {
            var cliente = await _contexto.Clientes.Include(c => c.Enderecos).FirstOrDefaultAsync(c => c.Nome == nome);
            if (cliente == null)
                throw new InvalidOperationException("Cliente não encontrado.");
            return cliente;
        }

        public async Task<IEnumerable<Clientes>> ObterTodosClientes()
        {
            return await _contexto.Clientes.Include(c => c.Enderecos).ToListAsync();
        }

        public async Task<IEnumerable<Clientes>> ObterPorEmail(string email)
        {
            return await _contexto.Clientes
                .Include(c => c.Enderecos) 
                .Where(c => c.Email == email)
                .Where(c => c.Ativo)
                .ToListAsync();
        }
    }
}