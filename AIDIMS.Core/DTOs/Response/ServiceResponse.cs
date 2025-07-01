using System;
using System.Collections.Generic;

namespace AIDIMS.Core.DTOs.Response
{
    /// <summary>
    /// DTO dùng để trả về thông tin của một dịch vụ
    /// </summary>
    public class ServiceResponse
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    /// <summary>
    /// DTO dùng để trả về danh sách các dịch vụ
    /// </summary>
    public class ServiceListResponse
    {
        public List<ServiceResponse> Services { get; set; } = new List<ServiceResponse>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    /// <summary>
    /// DTO dùng để trả về kết quả của các thao tác với Service
    /// </summary>
    public class ServiceActionResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ServiceResponse Service { get; set; }
    }
}