namespace AIDIMS.Core.DTOs.Response
{
    public class PatientAssignmentResponse
    {
        public int AssignmentID { get; set; }
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }
        public int? AppointmentID { get; set; }
        public int? MedicalRecordID { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public string AssignedDate { get; set; }
    }
}