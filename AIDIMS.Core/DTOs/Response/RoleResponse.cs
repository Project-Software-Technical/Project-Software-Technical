using System;
using System.Collections.Generic;

namespace AIDIMS.Core.DTOs.Response
{
    /// <summary>
    /// DTO dùng để trả về thông tin của một vai trò
    /// </summary>
    public class RoleResponse
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
    }

    /// <summary>
    /// DTO dùng để trả về danh sách các vai trò
    /// </summary>
    public class RoleListResponse
    {
        public List<RoleResponse> Roles { get; set; } = new List<RoleResponse>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    /// <summary>
    /// DTO dùng để trả về kết quả của các thao tác với Role
    /// </summary>
    public class RoleActionResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public RoleResponse Role { get; set; }
    }
}