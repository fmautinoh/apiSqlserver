using apiSqlserver.Models;
using apiSqlserver.Repositorio.IRepositorio;

namespace apiSqlserver.Repositorio;

public class vAutorRepositorio : ReaderRepositorio<VAutor>, IvautorRepositorio
{
    private readonly DatabaseContext _context;

    public vAutorRepositorio(DatabaseContext db) : base(db)
    {
        _context = db;
    }
}