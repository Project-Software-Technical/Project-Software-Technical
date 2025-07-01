using System;
using System.Collections.Generic;

namespace AIDIMS.Core.DTOs.Response
{
    /// <summary>
    /// DTO dùng để trả về thông tin của một người dùng
    /// </summary>
    public class UserResponse
    {
        public int UserID { get; set; }
        public int StaffID { get; set; }
        public int RoleID { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }


        // Thông tin liên quan
        public string RoleName { get; set; }
        public string StaffName { get; set; }
    }

    /// <summary>
    /// DTO dùng để trả về thông tin chi tiết của một người dùng
    /// </summary>
    public class UserDetailResponse : UserResponse
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public string RoleDescription { get; set; }
    }

    /// <summary>
    /// DTO dùng để trả về danh sách các người dùng
    /// </summary>
    public class UserListResponse
    {
        public List<UserResponse> Users { get; set; } = new List<UserResponse>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    /// <summary>
    /// DTO dùng để trả về kết quả của các thao tác với User
    /// </summary>
    public class UserActionResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public UserResponse User { get; set; }
    }
}