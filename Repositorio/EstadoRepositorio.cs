using apiSqlserver.Models;
using apiSqlserver.Repositorio.IRepositorio;

namespace apiSqlserver.Repositorio
{
    public class EstadoRepositorio : ReaderRepositorio<EstadoConservacion>, IEstadoRepositorio
    {
        private readonly DatabaseContext _context;

        public EstadoRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }
    }
}
