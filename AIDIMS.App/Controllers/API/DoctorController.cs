using System.Threading.Tasks;
using AIDIMS.Core.DTOs.Response;
using AIDIMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AIDIMS.App.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _service;

        public DoctorController(IDoctorService service)
        {
            _service = service;
        }

        // GET /api/Doctor
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _service.GetAllDoctorsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        // GET /api/Doctor/department/{departmentName}
        [HttpGet("department/{departmentName}")]
        public async Task<IActionResult> GetByDepartment(string departmentName, int pageNumber = 1, int pageSize = 10)
        {
            var result = await _service.GetDoctorsByDepartmentAsync(departmentName, pageNumber, pageSize);
            return Ok(result);
        }
    }
}