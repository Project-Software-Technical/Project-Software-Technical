using AIDIMS.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AIDIMS.Core.Data
{
    public class AIDIMSDbContext : DbContext
    {
        public AIDIMSDbContext(DbContextOptions<AIDIMSDbContext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<HospitalStaff> HospitalStaffs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ImagingRequest> ImagingRequests { get; set; }
        public DbSet<DiagnosisResult> DiagnosisResults { get; set; }
        public DbSet<DicomImage> DicomImages { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<PatientAssignment> PatientAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Định nghĩa mối quan hệ giữa các bảng
            modelBuilder.Entity<User>()
                .HasOne(u => u.HospitalStaff)
                .WithOne(h => h.User)
                .HasForeignKey<User>(u => u.StaffID);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleID);

            // Đảm bảo Email duy nhất
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(m => m.Patient)
                .WithMany(p => p.MedicalRecords)
                .HasForeignKey(m => m.PatientID);

            modelBuilder.Entity<ImagingRequest>()
                .HasOne(i => i.MedicalRecord)
                .WithMany(m => m.ImagingRequests)
                .HasForeignKey(i => i.RecordID);

            modelBuilder.Entity<ImagingRequest>()
                .HasOne(i => i.Service)
                .WithMany(s => s.ImagingRequests)
                .HasForeignKey(i => i.ServiceID);

            modelBuilder.Entity<DiagnosisResult>()
                .HasOne(d => d.MedicalRecord)
                .WithMany(m => m.DiagnosisResults)
                .HasForeignKey(d => d.RecordID);

            modelBuilder.Entity<DicomImage>()
                .HasOne(d => d.MedicalRecord)
                .WithMany(m => m.DicomImages)
                .HasForeignKey(d => d.RecordID);

            modelBuilder.Entity<DicomImage>()
                .HasOne(d => d.Technician)
                .WithMany(h => h.DicomImages)
                .HasForeignKey(d => d.TechnicianID);

            // Appointment - Patient (many-to-one)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientID);

            // Department unique Code
            modelBuilder.Entity<Department>()
                .HasIndex(d => d.Code).IsUnique();

            // MedicalRecord relationships
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(m => m.Receptionist)
                .WithMany()
                .HasForeignKey(m => m.CreatedBy);

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(m => m.Doctor)
                .WithMany()
                .HasForeignKey(m => m.DoctorID);

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(m => m.Department)
                .WithMany()
                .HasForeignKey(m => m.DepartmentID);

            // PatientAssignment relationships
            modelBuilder.Entity<PatientAssignment>()
                .HasOne(pa => pa.Patient)
                .WithMany(p => p.PatientAssignments)
                .HasForeignKey(pa => pa.PatientID);

            modelBuilder.Entity<PatientAssignment>()
                .HasOne(pa => pa.Doctor)
                .WithMany(d => d.PatientAssignments)
                .HasForeignKey(pa => pa.DoctorID);

            modelBuilder.Entity<PatientAssignment>()
                .HasOne(pa => pa.Appointment)
                .WithOne(a => a.PatientAssignment)
                .HasForeignKey<PatientAssignment>(pa => pa.AppointmentID);

            modelBuilder.Entity<PatientAssignment>()
                .HasOne(pa => pa.MedicalRecord)
                .WithOne(m => m.PatientAssignment)
                .HasForeignKey<PatientAssignment>(pa => pa.MedicalRecordID);

            // Appointment - MedicalRecord (one-to-one)
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(m => m.Appointment)
                .WithOne(a => a.MedicalRecord)
                .HasForeignKey<MedicalRecord>(m => m.AppointmentID);
        }
    }
}