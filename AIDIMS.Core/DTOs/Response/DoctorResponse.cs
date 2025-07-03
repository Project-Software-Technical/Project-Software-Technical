namespace AIDIMS.Core.DTOs.Response
{
    public class DoctorResponse
    {
        public int DoctorID { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Department { get; set; }
        public string Specialization { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsAvailable { get; set; }
    }
}