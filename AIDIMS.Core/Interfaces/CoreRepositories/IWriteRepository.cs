using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIDIMS.Core.Interfaces
{
    public interface IWriteRepository<T> where T : class
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
        Task<T> UpdateAsync(int id, T entity);


        /// <summary>
        /// Lưu các thay đổi vào cơ sở dữ liệu
        /// </summary>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        Task<int> SaveChangesAsync();
    }
}