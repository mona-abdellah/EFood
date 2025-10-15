using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Food.Apllication.Contracts
{
    public interface IGenericRepository<TEntity,TId>
    {
        public Task<TEntity> CreateAsync(TEntity entity);
        public Task<TEntity> UpdateAsync(TEntity entity);
        public Task<TEntity> DeleteAsync(TEntity entity);
        public Task<IQueryable<TEntity>> GetAllAsync();
        public Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity,bool>> expression);
        public ValueTask<TEntity> GetOneAsync(TId id);
        public Task<int> SaveChangesAsync();

    }
}
