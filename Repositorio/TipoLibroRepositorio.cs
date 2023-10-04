using apiSqlserver.Models;
using apiSqlserver.Repositorio.IRepositorio;

namespace apiSqlserver.Repositorio
{
    public class TipoLibroRepositorio : ReaderRepositorio<TipoLibro>, ITipoLibroRepositorio
    {
        private readonly DatabaseContext _context;

        public TipoLibroRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }
    }
}
