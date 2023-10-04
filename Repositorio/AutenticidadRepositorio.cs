using apiSqlserver.Models;
using apiSqlserver.Repositorio.IRepositorio;

namespace apiSqlserver.Repositorio
{
    public class AutenticidadRepositorio : ReaderRepositorio<Autenticidad>, IAutenticidadRepositorio
    {
        private readonly DatabaseContext _context;

        public AutenticidadRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }
    }
}
