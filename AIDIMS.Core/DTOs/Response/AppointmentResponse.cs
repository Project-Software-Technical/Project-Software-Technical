using System;

namespace AIDIMS.Core.DTOs.Response
{
    public class AppointmentResponse
    {
        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string? Note { get; set; }
        public string Status { get; set; }
        public string Department { get; set; }

        // related
        public string PatientName { get; set; }
    }
}