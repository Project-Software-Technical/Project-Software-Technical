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
    public class DiagnosisResultController : ControllerBase
    {
        private readonly IDiagnosisResultService _diagnosisResultService;

        public DiagnosisResultController(IDiagnosisResultService diagnosisResultService)
        {
            _diagnosisResultService = diagnosisResultService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _diagnosisResultService.GetAllAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _diagnosisResultService.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetDetailById(int id)
        {
            var result = await _diagnosisResultService.GetDiagnosisResultDetailAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("record/{recordId}")]
        public async Task<IActionResult> GetByRecordId(int recordId, int pageNumber = 1, int pageSize = 10)
        {
            var result = await _diagnosisResultService.GetDiagnosisResultsByRecordIdAsync(recordId, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDiagnosisResultRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _diagnosisResultService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.DiagnosisResultID }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateDiagnosisResultRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _diagnosisResultService.UpdateAsync(id, request);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _diagnosisResultService.DeleteByIdAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            var count = await _diagnosisResultService.CountAsync();
            return Ok(count);
        }
    }
}