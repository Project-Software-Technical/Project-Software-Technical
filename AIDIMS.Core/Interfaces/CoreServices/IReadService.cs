using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AIDIMS.Core.Interfaces
{
    public class PagedResponse<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    }



    public interface IReadService<T, TResponse> where T : class where TResponse : class
    {


        /// <summary>
        /// Lấy danh sách có phân trang dưới dạng DTO
        /// </summary>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <returns>Danh sách các DTO được phân trang</returns>
        Task<PagedResponse<TResponse>> GetAllAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Lấy bản ghi theo Id dưới dạng DTO
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>DTO theo Id</returns>
        Task<TResponse> GetByIdAsync(int id);

        /// <summary>
        /// Lấy tổng số bản ghi
        /// </summary>
        /// <returns>Tổng số bản ghi</returns>
        Task<int> CountAsync();
    }
}
