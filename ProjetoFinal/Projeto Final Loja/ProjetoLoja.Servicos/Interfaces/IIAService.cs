namespace ProjetoLoja.Servicos
{
    public interface IIAService
    {
        Task<string> Perguntar(string pergunta, string usuarioId);
    
    }
}