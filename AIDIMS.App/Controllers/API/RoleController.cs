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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _roleService.GetAllAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _roleService.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _roleService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.RoleID }, result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateRoleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _roleService.UpdateAsync(id, request);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _roleService.DeleteByIdAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            var count = await _roleService.CountAsync();
            return Ok(count);
        }
    }
}