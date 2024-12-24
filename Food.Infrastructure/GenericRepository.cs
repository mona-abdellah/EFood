using Food.Apllication.Contracts;
using Food.Context;
using Food.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Infrastructure
{
    public class GenericRepository<TEntity,TId> : IGenericRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        private readonly FoodContext context;
        private readonly DbSet<TEntity> dbset;
        public GenericRepository(FoodContext _context)
        {
            this.context = _context;
            this.dbset =context.Set<TEntity>();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            return (await dbset.AddAsync(entity)).Entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            return   context.Remove(entity).Entity;
        }

        public Task<IQueryable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(dbset.Select(p => p));
        }

        public ValueTask<TEntity> GetOneAsync(TId id)
        {
            return dbset.FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            return  Task.FromResult(dbset.Update(entity).Entity);
        }
    }
}
