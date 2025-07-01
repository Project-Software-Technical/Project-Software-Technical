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
    public class DicomImageController : ControllerBase
    {
        private readonly IDicomImageService _dicomImageService;

        public DicomImageController(IDicomImageService dicomImageService)
        {
            _dicomImageService = dicomImageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _dicomImageService.GetAllAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _dicomImageService.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetDetailById(int id)
        {
            var result = await _dicomImageService.GetDicomImageDetailAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("record/{recordId}")]
        public async Task<IActionResult> GetByRecordId(int recordId, int pageNumber = 1, int pageSize = 10)
        {
            var result = await _dicomImageService.GetDicomImagesByRecordIdAsync(recordId, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDicomImageRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _dicomImageService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.ImageID }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateDicomImageRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _dicomImageService.UpdateAsync(id, request);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _dicomImageService.DeleteByIdAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            var count = await _dicomImageService.CountAsync();
            return Ok(count);
        }

        [HttpPut("{id}/approve")]
        public async Task<IActionResult> Approve(int id, [FromQuery] bool isApproved, [FromQuery] string doctorNotes)
        {
            var result = await _dicomImageService.ApproveDicomImageAsync(id, isApproved, doctorNotes);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}/aifeedback")]
        public async Task<IActionResult> UpdateAIFeedback(int id, [FromQuery] string aiFeedback)
        {
            var result = await _dicomImageService.UpdateAIFeedbackAsync(id, aiFeedback);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}