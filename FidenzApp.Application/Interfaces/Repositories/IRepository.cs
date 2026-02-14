using System.Linq.Expressions;

namespace FidenzApp.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T:class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);

        Task<T> GetSync(Expression<Func<T, bool>> filter);

        Task addSync(T entity);

        Task saveAsync ();
    }
}
