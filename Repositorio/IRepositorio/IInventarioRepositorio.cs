using apiSqlserver.Models;

namespace apiSqlserver.Repositorio.IRepositorio
{
    public interface IInventarioRepositorio : IRepositorio<InventarioLibro>
    {
        Task<InventarioLibro> Actualizar(InventarioLibro entidad);
    }
}
