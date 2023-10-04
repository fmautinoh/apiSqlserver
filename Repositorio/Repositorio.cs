using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using apiSqlserver.Repositorio.IRepositorio;
using apiSqlserver.Models;

namespace apiSqlserver.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly DatabaseContext _context;
        internal DbSet<T> dbSet;

        public Repositorio(DatabaseContext db)
        {
            _context = db;
            dbSet = _context.Set<T>();
        }

        public async Task Crear(T entidad)
        {
            await dbSet.AddAsync(entidad);
            await Grabar();
        }

        public async Task Grabar()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<T> Listar(Expression<Func<T, bool>>? filtro = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> ListObjetos(Expression<Func<T, bool>>? filtro = null)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            return await query.ToListAsync();
        }

        public async Task Remover(T entidad)
        {
            dbSet.Remove(entidad);
            await Grabar();
        }
        
        public async Task<T> ObtenerPrimerElementoDescendente(Expression<Func<T, bool>>? filtro = null, Expression<Func<T, object>> ordenarPor = null)
        {
            IQueryable<T> query = dbSet;

            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            if (ordenarPor != null)
            {
                query = query.OrderByDescending(ordenarPor);
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}
