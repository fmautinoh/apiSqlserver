using apiSqlserver.Models;
using apiSqlserver.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

namespace apiSqlserver.Repositorio
{
    public class LibroRepositorio : Repositorio<Libro>, ILibroRepositorio
    {
        private readonly DatabaseContext _context;

        public LibroRepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }

        public async Task<Libro> Actualizar(Libro entidad)
        {
            _context.Libros.Update(entidad);
            await _context.SaveChangesAsync();
            return entidad;
        }
    }
}
