using System;
using System.Collections.Generic;

namespace AIDIMS.Core.DTOs.Response
{
    /// <summary>
    /// DTO dùng để trả về thông tin của một bệnh nhân
    /// </summary>
    public class PatientResponse
    {
        public int PatientID { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    /// <summary>
    /// DTO dùng để trả về danh sách các bệnh nhân
    /// </summary>
    public class PatientListResponse
    {
        public List<PatientResponse> Patients { get; set; } = new List<PatientResponse>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    /// <summary>
    /// DTO dùng để trả về kết quả của các thao tác với Patient
    /// </summary>
    public class PatientActionResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public PatientResponse Patient { get; set; }
    }
}