using apiSqlserver.Models;
using apiSqlserver.Repositorio.IRepositorio;

namespace apiSqlserver.Repositorio
{
    public class vInventarioRepositorio : ReaderRepositorio<VInventario>, IvInventarioRepositorio
    {
        private readonly DatabaseContext _context;

        public vInventarioRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }
    }
}
