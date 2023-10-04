using apiSqlserver.Models;
using apiSqlserver.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

namespace apiSqlserver.Repositorio
{
    public class LibroxAutorRepositorio : Repositorio<LibrosAutore>, ILibroxAutorRepositorio
    {
        private readonly DatabaseContext _context;

        public LibroxAutorRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }

        public async Task<LibrosAutore> Actualizar(LibrosAutore entidad)
        {
            _context.LibrosAutores.Update(entidad);
            await _context.SaveChangesAsync();
            return entidad;
        }
    }
}
