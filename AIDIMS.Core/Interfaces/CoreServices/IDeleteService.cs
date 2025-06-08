using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIDIMS.Core.Interfaces
{
    public interface IDeleteService<T> where T : class
    {
        /// <summary>
        /// Xóa một bản ghi theo Id
        /// </summary>
        /// <param name="id">Id của bản ghi cần xóa</param>
        /// <returns>True nếu xóa thành công, false nếu thất bại</returns>
        Task<bool> DeleteByIdAsync(int id);
    }
}