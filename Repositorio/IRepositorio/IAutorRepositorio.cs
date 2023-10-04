using apiSqlserver.Models;

namespace apiSqlserver.Repositorio.IRepositorio
{
    public interface IAutorRepositorio : IRepositorio<Autore>
    {
        Task<Autore> Actualizar(Autore entidad);
    }
}
