using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiSqlserver.Models;
using apiSqlserver.Models.ModelsDto;
using apiSqlserver.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

namespace apiSqlserver.Repositorio
{
    public class vlibrorepositorio : ReaderRepositorio<VLibro>, Ivlibrorepositorio
    {
        private readonly DatabaseContext _context;

        public vlibrorepositorio(DatabaseContext db) : base(db)
        {
            _context = db;
        }

        // Implement the ListLibrosConAutores method
        public async Task<List<VLibro>> ListLibrosConAutores()
        {
            // Fetch the grouped books without including the authors
            var librosGrouped = await _context.VLibros
                .GroupBy(libro => libro.LibroId)
                .Select(group => new VLibro
                {
                    LibroId = group.Key,
                    NombreLib = group.First().NombreLib,
                    TipoLibroId = group.First().TipoLibroId,
                    TipoLibro = group.First().TipoLibro,
                    Edicion = group.First().Edicion,
                    Año = group.First().Año,
                    Editorial = group.First().Editorial,
                })
                .ToListAsync();

            // Fetch the authors separately
            var autores = await _context.VLibros
                .Select(libro => new { libro.LibroId, libro.AutorId, libro.NombreAutor })
                .Distinct()
                .ToListAsync();

            // Map the authors to the corresponding books
            foreach (var libro in librosGrouped)
            {
                libro.AutoresIds = autores
                    .Where(a => a.LibroId == libro.LibroId && a.AutorId != 0) // Exclude entries with autorId = 0
                    .Select(a => new AutorDtosList { AutorId = (int)a.AutorId, NombreAutor = a.NombreAutor })
                    .ToList();
            }

            return librosGrouped;
        }
        
        public async Task<VLibro> GetLibroConAutoresPorLibroid(int libroid)
        {
            // Fetch the grouped book without including the authors
            var libroGrouped = await _context.VLibros
                .Where(libro => libro.LibroId == libroid)
                .Select(group => new VLibro
                {
                    LibroId = group.LibroId,
                    NombreLib = group.NombreLib,
                    TipoLibroId = group.TipoLibroId,
                    TipoLibro = group.TipoLibro,
                    Edicion = group.Edicion,
                    Año = group.Año,
                    Editorial = group.Editorial,
                    AutoresIds = new List<AutorDtosList>() // Initialize an empty list for AutoresIds
                })
                .FirstOrDefaultAsync();

            if (libroGrouped != null)
            {
                // Fetch the authors for the specified libro
                var autores = await _context.VLibros
                    .Where(libro => libro.LibroId == libroid && libro.AutorId != 0) // Exclude entries with autorId = 0
                    .Select(a => new AutorDtosList { AutorId = (int)a.AutorId, NombreAutor = a.NombreAutor })
                    .ToListAsync();

                libroGrouped.AutoresIds.AddRange(autores); // Add authors to the AutoresIds list
            }

            return libroGrouped;
        }

    }
}
