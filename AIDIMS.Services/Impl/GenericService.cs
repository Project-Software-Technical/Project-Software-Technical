using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AIDIMS.Core.Interfaces;

namespace AIDIMS.Services.Impl
{
    public abstract class GenericService<T> : ICoreService<T> where T : class
    {
        protected readonly ICoreRepository<T> _repository;

        protected GenericService(ICoreRepository<T> repository)
        {
            _repository = repository;
        }

        #region ReadService

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _repository.GetAllAsync(pageNumber, pageSize);
        }

        public virtual async Task<T?> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public virtual async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        #endregion

        #region WriteService

        public virtual async Task<T> AddAsync(T entity)
        {
            return await _repository.AddAsync(entity);
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        #endregion

        #region DeleteService

        public virtual async Task<bool> DeleteByIdAsync(string id)
        {
            return await _repository.DeleteByIdAsync(id);
        }

        #endregion
    }
}