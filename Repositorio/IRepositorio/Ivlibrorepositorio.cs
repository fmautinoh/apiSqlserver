using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiSqlserver.Models;
using apiSqlserver.Models;
using apiSqlserver.Repositorio.IRepositorio;

namespace apiSqlserver.Repositorio.IRepositorio
{
    public interface Ivlibrorepositorio : IReaderRepositorio<VLibro>
    {
        // Add a new method to fetch books with multiple authors
        Task<List<VLibro>> ListLibrosConAutores();
        Task<VLibro> GetLibroConAutoresPorLibroid(int libroid);
    }
}
