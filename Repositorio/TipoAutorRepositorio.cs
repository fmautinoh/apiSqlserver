using apiSqlserver.Models;
using apiSqlserver.Repositorio.IRepositorio;

namespace apiSqlserver.Repositorio
{
    public class TipoAutorRepositorio : ReaderRepositorio<TipoAutor>, ITipoAutorRepositorio
    {
        private readonly DatabaseContext _context;

        public TipoAutorRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }
    }
}
