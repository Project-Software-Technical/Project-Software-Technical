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

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(m => m.HospitalStaff)
                .WithMany(h => h.MedicalRecords)
                .HasForeignKey(m => m.StaffID);

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
        }
    }
}