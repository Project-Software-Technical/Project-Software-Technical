using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIDIMS.Core.Interfaces
{
    public interface IWriteService<T> where T : class
    {
        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <returns>Đối tượng đã được thêm vào cơ sở dữ liệu</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Cập nhật một bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần cập nhật</param>
        /// <returns>Đối tượng đã được cập nhật</returns>
        Task<T> UpdateAsync(T entity);
    }
}