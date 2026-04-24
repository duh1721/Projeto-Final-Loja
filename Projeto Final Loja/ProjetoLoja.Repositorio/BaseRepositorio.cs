public abstract class BaseRepositorio
{
    protected readonly ProjetoLojaContexto _contexto;

    public BaseRepositorio(ProjetoLojaContexto contexto)
    {
        _contexto = contexto;
    }
}