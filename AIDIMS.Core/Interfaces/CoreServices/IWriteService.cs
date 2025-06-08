using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIDIMS.Core.Interfaces
{

    public interface IWriteService<T, TCreateRequest, TUpdateRequest, TResponse>
        where T : class
        where TCreateRequest : class
        where TUpdateRequest : class
        where TResponse : class
    {
        /// <summary>
        /// Thêm mới một bản ghi từ DTO
        /// </summary>
        /// <param name="createRequest">DTO chứa thông tin cần thêm</param>
        /// <returns>DTO của đối tượng đã được thêm vào cơ sở dữ liệu</returns>
        Task<TResponse> CreateAsync(TCreateRequest createRequest);

        /// <summary>
        /// Cập nhật một bản ghi từ DTO
        /// </summary>
        /// <param name="updateRequest">DTO chứa thông tin cập nhật</param>
        /// <returns>DTO của đối tượng đã được cập nhật</returns>
        Task<TResponse> UpdateAsync(int id, TUpdateRequest updateRequest);
    }
}