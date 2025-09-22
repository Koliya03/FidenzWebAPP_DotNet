using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
