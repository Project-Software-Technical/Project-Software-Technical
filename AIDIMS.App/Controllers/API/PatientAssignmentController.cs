using System.Threading.Tasks;
using AIDIMS.Core.DTOs.Request;
using AIDIMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AIDIMS.App.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientAssignmentController : ControllerBase
    {
        private readonly IPatientAssignmentService _service;

        public PatientAssignmentController(IPatientAssignmentService service)
        {
            _service = service;
        }

        // POST /api/PatientAssignment
        [HttpPost]
        public async Task<IActionResult> Create(CreatePatientAssignmentRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.AssignmentID }, result);
        }

        // GET /api/PatientAssignment/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // GET /api/PatientAssignment/doctor/{doctorId}
        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctor(int doctorId, int pageNumber = 1, int pageSize = 10)
        {
            var result = await _service.GetAssignmentsByDoctorAsync(doctorId, pageNumber, pageSize);
            return Ok(result);
        }
    }
}