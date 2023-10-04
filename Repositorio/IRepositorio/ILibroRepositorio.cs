using apiSqlserver.Models;

namespace apiSqlserver.Repositorio.IRepositorio
{
    public interface ILibroRepositorio : IRepositorio<Libro>
    {
        Task<Libro> Actualizar(Libro entidad);

    }
}
