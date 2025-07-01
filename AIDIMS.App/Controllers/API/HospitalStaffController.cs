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
    public class HospitalStaffController : ControllerBase
    {
        private readonly IHospitalStaffService _hospitalStaffService;

        public HospitalStaffController(IHospitalStaffService hospitalStaffService)
        {
            _hospitalStaffService = hospitalStaffService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _hospitalStaffService.GetAllAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _hospitalStaffService.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateHospitalStaffRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _hospitalStaffService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.StaffID }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateHospitalStaffRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _hospitalStaffService.UpdateAsync(id, request);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _hospitalStaffService.DeleteByIdAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            var count = await _hospitalStaffService.CountAsync();
            return Ok(count);
        }
    }
}