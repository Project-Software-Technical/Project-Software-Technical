using System.Threading.Tasks;
using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.DTOs.Response;
using AIDIMS.Core.Interfaces;
using AIDIMS.Core.Models;

namespace AIDIMS.Services.Interfaces
{
    public interface IUserService : ICoreService<User, CreateUserRequest, UpdateUserRequest, UserResponse>
    {
        /// <summary>
        /// Lấy thông tin chi tiết của người dùng theo ID
        /// </summary>
        /// <param name="id">ID của người dùng</param>
        /// <returns>Thông tin chi tiết của người dùng</returns>
        Task<UserDetailResponse> GetUserDetailAsync(int id);

        /// <summary>
        /// Đổi mật khẩu của người dùng
        /// </summary>
        /// <param name="userId">ID của người dùng</param>
        /// <param name="request">Thông tin đổi mật khẩu</param>
        /// <returns>Kết quả đổi mật khẩu</returns>
        Task<UserActionResponse> ChangePasswordAsync(int userId, ChangePasswordRequest request);

        /// <summary>
        /// Xác thực đăng nhập của người dùng
        /// </summary>
        /// <param name="request">Thông tin đăng nhập</param>
        /// <returns>Kết quả đăng nhập</returns>
        Task<UserActionResponse> LoginAsync(LoginRequest request);

        /// <summary>
        /// Lấy thông tin người dùng theo StaffID
        /// </summary>
        Task<UserResponse?> GetByStaffIdAsync(int staffId);

        /// <summary>
        /// Cập nhật người dùng theo StaffID
        /// </summary>
        Task<UserResponse?> UpdateByStaffIdAsync(int staffId, UpdateUserRequest request);
    }
}