using System.Collections.Generic;

namespace AIDIMS.Core.DTOs.Response
{
    /// <summary>
    /// DTO trả về thông tin Department
    /// </summary>
    public class DepartmentResponse
    {
        public int DepartmentID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// DTO trả về danh sách Department
    /// </summary>
    public class DepartmentListResponse
    {
        public IEnumerable<DepartmentResponse> Departments { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    /// <summary>
    /// DTO trả về kết quả hành động
    /// </summary>
    public class DepartmentActionResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public DepartmentResponse Department { get; set; }
    }
}