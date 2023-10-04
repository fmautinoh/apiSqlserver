using System.Linq.Expressions;

namespace apiSqlserver.Repositorio.IRepositorio
{
    public interface IReaderRepositorio<T> where T : class
    {
        Task<List<T>> ListObjetos(Expression<Func<T, bool>>? filtro = null);
        Task<T> Listar(Expression<Func<T, bool>>? filtro = null, bool tracked = true);
        Task<T> ObtenerPrimerElementoDescendente(Expression<Func<T, bool>>? filtro = null, Expression<Func<T, object>> ordenarPor = null);
    }
}
