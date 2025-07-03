using System;
using System.Threading.Tasks;
using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.DTOs.Response;
using AIDIMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AIDIMS.App.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagingRequestController : ControllerBase
    {
        private readonly IImagingRequestService _imagingRequestService;

        public ImagingRequestController(IImagingRequestService imagingRequestService)
        {
            _imagingRequestService = imagingRequestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _imagingRequestService.GetAllAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _imagingRequestService.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetDetailById(int id)
        {
            var result = await _imagingRequestService.GetImagingRequestDetailAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("record/{recordId}")]
        public async Task<IActionResult> GetByRecordId(int recordId, int pageNumber = 1, int pageSize = 10)
        {
            var result = await _imagingRequestService.GetImagingRequestsByRecordIdAsync(recordId, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetByStatus(string status, int pageNumber = 1, int pageSize = 10)
        {
            var result = await _imagingRequestService.GetImagingRequestsByStatusAsync(status, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("technician/{techId}")]
        public async Task<IActionResult> GetByTechnician(int techId, int pageNumber = 1, int pageSize = 10)
        {
            var result = await _imagingRequestService.GetImagingRequestsByTechnicianAsync(techId, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateImagingRequestRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _imagingRequestService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.RequestID }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateImagingRequestRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _imagingRequestService.UpdateAsync(id, request);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _imagingRequestService.DeleteByIdAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            var count = await _imagingRequestService.CountAsync();
            return Ok(count);
        }
    }
}