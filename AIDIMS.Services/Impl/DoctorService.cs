using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AIDIMS.Core.DTOs.Response;
using AIDIMS.Core.Interfaces;
using AIDIMS.Repositories.Interfaces;
using AIDIMS.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AIDIMS.Services.Impl
{
    public class DoctorService : IDoctorService
    {
        private readonly IHospitalStaffRepository _staffRepository;
        private readonly AIDIMS.Core.Data.AIDIMSDbContext _dbContext;
        private readonly IMapper _mapper;

        public DoctorService(IHospitalStaffRepository staffRepository, AIDIMS.Core.Data.AIDIMSDbContext dbContext, IMapper mapper)
        {
            _staffRepository = staffRepository;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PagedResponse<DoctorResponse>> GetAllDoctorsAsync(int pageNumber, int pageSize)
        {
            var doctors = await _dbContext.HospitalStaffs
                .Include(s => s.Department)
                .Include(s => s.User)
                    .ThenInclude(u => u.Role)
                .Where(s => s.User != null && s.User.Role.RoleName == "Doctor")
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var list = _mapper.Map<IEnumerable<DoctorResponse>>(doctors);
            return new PagedResponse<DoctorResponse>
            {
                Items = list,
                TotalCount = list.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PagedResponse<DoctorResponse>> GetDoctorsByDepartmentAsync(string departmentName, int pageNumber, int pageSize)
        {
            var query = _dbContext.HospitalStaffs
                .Include(s => s.Department)
                .Include(s => s.User)
                    .ThenInclude(u => u.Role)
                .Where(s => s.User != null && s.User.Role.RoleName == "Doctor" &&
                              s.Department != null && s.Department.Name.ToLower() == departmentName.ToLower());

            var total = await query.CountAsync();
            var doctors = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var list = _mapper.Map<IEnumerable<DoctorResponse>>(doctors);
            return new PagedResponse<DoctorResponse>
            {
                Items = list,
                TotalCount = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}