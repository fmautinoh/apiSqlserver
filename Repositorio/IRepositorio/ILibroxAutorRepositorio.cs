using apiSqlserver.Models;

namespace apiSqlserver.Repositorio.IRepositorio
{
    public interface ILibroxAutorRepositorio : IRepositorio<LibrosAutore>
    {
        Task<LibrosAutore> Actualizar(LibrosAutore entidad);
    }
}
