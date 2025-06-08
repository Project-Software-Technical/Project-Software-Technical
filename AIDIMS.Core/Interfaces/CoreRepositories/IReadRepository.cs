using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AIDIMS.Core.Interfaces
{
    public interface IReadRepository<T> where T : class
    {


        /// <summary>
        /// Lấy danh sách có phân trang
        /// </summary>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <returns>Danh sách các bản ghi được phân trang</returns>
        Task<IEnumerable<T>> GetAllAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Bản ghi theo Id</returns>
        Task<T?> GetByIdAsync(int id);


        /// <summary>
        /// Lấy tổng số bản ghi
        /// </summary>
        /// <returns>Tổng số bản ghi</returns>
        Task<int> CountAsync();
    }
}