using FidenzApp.Application.Interfaces.Repositories;
using FidenzApp.Domain.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FidenzApp.Infranstructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        private Microsoft.EntityFrameworkCore.DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetSync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task addSync(T entity)
        {
           await dbSet.AddAsync(entity);
        }

        public async Task saveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
