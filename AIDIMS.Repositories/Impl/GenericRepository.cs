using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AIDIMS.Core.Data;
using AIDIMS.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AIDIMS.Repositories.Impl
{
    public abstract class GenericRepository<T> : ICoreRepository<T> where T : class
    {
        protected readonly AIDIMSDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        protected GenericRepository(AIDIMSDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        #region ReadRepository

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _dbSet
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        #endregion

        #region WriteRepository

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await SaveChangesAsync();
            return entity;
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        #endregion

        #region DeleteRepository

        public virtual async Task<bool> DeleteByIdAsync(string id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }
            _dbSet.Remove(entity);
            await SaveChangesAsync();
            return true;
        }

        #endregion
    }
}