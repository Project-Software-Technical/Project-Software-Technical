using AIDIMS.Core.Data;
using AIDIMS.Core.Models;
using AIDIMS.Repositories.Interfaces;

namespace AIDIMS.Repositories.Impl
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(AIDIMSDbContext dbContext) : base(dbContext)
        {
        }
    }
}