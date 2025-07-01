using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.DTOs.Response;
using AIDIMS.Core.Interfaces;
using AIDIMS.Core.Models;
using AIDIMS.Repositories.Interfaces;
using AIDIMS.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AIDIMS.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<int> CountAsync()
        {
            return _repository.CountAsync();
        }

        public async Task<UserResponse> CreateAsync(CreateUserRequest createRequest)
        {
            var user = _mapper.Map<User>(createRequest);
            user.CreatedDate = DateTime.UtcNow;
            user.Status = user.Status ?? "Hoạt động";

            // Kiểm tra StaffID đã có tài khoản chưa
            var existingByStaff = await _repository.GetByStaffIdAsync(createRequest.StaffID);
            if (existingByStaff != null)
            {
                throw new InvalidOperationException("Nhân viên này đã có tài khoản người dùng.");
            }

            // TODO: Mã hóa mật khẩu trước khi lưu vào DB

            var result = await _repository.AddAsync(user);
            return _mapper.Map<UserResponse>(result);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            return await _repository.DeleteByIdAsync(id);
        }

        public async Task<PagedResponse<UserResponse>> GetAllAsync(int pageNumber, int pageSize)
        {
            var entities = await _repository.GetAllAsync(pageNumber, pageSize);
            return new PagedResponse<UserResponse>
            {
                Items = _mapper.Map<IEnumerable<UserResponse>>(entities),
                TotalCount = await _repository.CountAsync(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<UserResponse> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<UserResponse>(entity);
        }

        public async Task<UserDetailResponse> GetUserDetailAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return null;

            var detailResponse = _mapper.Map<UserDetailResponse>(user);

            // Lấy thêm các thông tin liên quan từ staff và role
            if (user.HospitalStaff != null)
            {
                detailResponse.FullName = user.HospitalStaff.FullName;
                detailResponse.Email = user.HospitalStaff.Email;
                detailResponse.Phone = user.HospitalStaff.Phone;
                detailResponse.Position = user.HospitalStaff.Position;
            }

            if (user.Role != null)
            {
                detailResponse.RoleName = user.Role.RoleName;
                detailResponse.RoleDescription = user.Role.Description;
            }

            return detailResponse;
        }

        public async Task<UserResponse> UpdateAsync(int id, UpdateUserRequest updateRequest)
        {
            // Lấy thông tin hiện tại của user
            var existingUser = await _repository.GetByIdAsync(id);
            if (existingUser == null)
            {
                return null;
            }

            // Map thông tin từ request vào entity hiện có
            var userToUpdate = _mapper.Map<User>(updateRequest);
            userToUpdate.UserID = id;
            userToUpdate.StaffID = existingUser.StaffID; // Giữ nguyên StaffID
            userToUpdate.CreatedDate = existingUser.CreatedDate;

            // Nếu không có cập nhật email, giữ nguyên email cũ
            if (string.IsNullOrEmpty(userToUpdate.Email))
            {
                userToUpdate.Email = existingUser.Email;
            }

            // Nếu không có cập nhật mật khẩu, giữ nguyên mật khẩu cũ
            if (string.IsNullOrEmpty(userToUpdate.Password))
            {
                userToUpdate.Password = existingUser.Password;
            }
            else
            {
                // TODO: Mã hóa mật khẩu mới trước khi cập nhật
            }

            // Cập nhật thông tin
            var result = await _repository.UpdateAsync(id, userToUpdate);
            return _mapper.Map<UserResponse>(result);
        }

        public async Task<UserActionResponse> ChangePasswordAsync(int userId, ChangePasswordRequest request)
        {
            var user = await _repository.GetByIdAsync(userId);
            if (user == null)
            {
                return new UserActionResponse
                {
                    Success = false,
                    Message = "Không tìm thấy người dùng",
                    User = null
                };
            }

            // TODO: Thực hiện kiểm tra mật khẩu hiện tại và mã hóa mật khẩu mới
            bool currentPasswordValid = user.Password == request.CurrentPassword;

            if (!currentPasswordValid)
            {
                return new UserActionResponse
                {
                    Success = false,
                    Message = "Mật khẩu hiện tại không chính xác",
                    User = null
                };
            }

            // Cập nhật mật khẩu mới
            user.Password = request.NewPassword; // TODO: Cần mã hóa trước khi lưu

            await _repository.UpdateAsync(userId, user);

            return new UserActionResponse
            {
                Success = true,
                Message = "Đổi mật khẩu thành công",
                User = _mapper.Map<UserResponse>(user)
            };
        }

        public async Task<UserActionResponse> LoginAsync(LoginRequest request)
        {
            var user = await _repository.GetByEmailAsync(request.Email);
            if (user == null || user.Password != request.Password)
            {
                return new UserActionResponse
                {
                    Success = false,
                    Message = "Tên đăng nhập hoặc mật khẩu không chính xác",
                    User = null
                };
            }

            return new UserActionResponse
            {
                Success = true,
                Message = "Đăng nhập thành công",
                User = _mapper.Map<UserResponse>(user)
            };
        }

        public async Task<UserResponse?> GetByStaffIdAsync(int staffId)
        {
            var user = await _repository.GetByStaffIdAsync(staffId);
            return _mapper.Map<UserResponse>(user);
        }

        public async Task<UserResponse?> UpdateByStaffIdAsync(int staffId, UpdateUserRequest request)
        {
            var user = await _repository.GetByStaffIdAsync(staffId);
            if (user == null)
                return null;

            var updatedEntity = _mapper.Map<User>(request);
            updatedEntity.UserID = user.UserID;
            updatedEntity.StaffID = staffId;
            updatedEntity.Email = string.IsNullOrEmpty(updatedEntity.Email) ? user.Email : updatedEntity.Email;
            updatedEntity.CreatedDate = user.CreatedDate;

            if (string.IsNullOrEmpty(updatedEntity.Password))
                updatedEntity.Password = user.Password;

            var result = await _repository.UpdateAsync(user.UserID, updatedEntity);
            return _mapper.Map<UserResponse>(result);
        }
    }
}