using apiSqlserver.Models;
using apiSqlserver.Repositorio;
using apiSqlserver.Repositorio.IRepositorio.IReporteRepositorio;

namespace apiSqlserver.Repositorio.ReporteRepositorio
{
    public class InvReporteRepositorio : ReaderRepositorio<VInvReporte>, IRepoteInventarioRepositorio
    {
        private readonly DatabaseContext _context;

        public InvReporteRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }
    }
}
