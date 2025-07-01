using System;
using System.Collections.Generic;

namespace AIDIMS.Core.DTOs.Response
{
    /// <summary>
    /// DTO dùng để trả về thông tin của một nhân viên y tế
    /// </summary>
    public class HospitalStaffResponse
    {
        public int StaffID { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    /// <summary>
    /// DTO dùng để trả về danh sách các nhân viên y tế
    /// </summary>
    public class HospitalStaffListResponse
    {
        public List<HospitalStaffResponse> HospitalStaffs { get; set; } = new List<HospitalStaffResponse>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    /// <summary>
    /// DTO dùng để trả về kết quả của các thao tác với HospitalStaff
    /// </summary>
    public class HospitalStaffActionResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HospitalStaffResponse HospitalStaff { get; set; }
    }
}