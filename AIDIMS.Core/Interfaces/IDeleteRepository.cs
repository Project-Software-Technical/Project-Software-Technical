using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIDIMS.Core.Interfaces
{
    public interface IDeleteRepository<T> where T : class
    {
        /// <summary>
        /// Xóa một bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần xóa</param>
        /// <returns>True nếu xóa thành công, false nếu thất bại</returns>
        Task<bool> DeleteAsync(T entity);
        
        /// <summary>
        /// Xóa một bản ghi theo Id
        /// </summary>
        /// <param name="id">Id của bản ghi cần xóa</param>
        /// <returns>True nếu xóa thành công, false nếu thất bại</returns>
        Task<bool> DeleteByIdAsync(string id);
        
        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="entities">Danh sách đối tượng cần xóa</param>
        /// <returns>True nếu xóa thành công, false nếu thất bại</returns>
        Task<bool> DeleteRangeAsync(IEnumerable<T> entities);
    }
} 