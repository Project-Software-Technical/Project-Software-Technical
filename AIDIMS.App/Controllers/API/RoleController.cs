using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AIDIMS.Core.Models;
using AIDIMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AIDIMS.App.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IRoleService roleService, ILogger<RoleController> logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetAllRoles()
        {
            try
            {
                var roles = await _roleService.GetAllAsync();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all roles");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("paged")]
        public async Task<ActionResult<IEnumerable<Role>>> GetPagedRoles(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (pageNumber < 1 || pageSize < 1)
                {
                    return BadRequest("Page number and page size must be greater than 0");
                }

                var roles = await _roleService.GetAllAsync(pageNumber, pageSize);
                var count = await _roleService.CountAsync();

                var result = new
                {
                    Total = count,
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                    Roles = roles
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting paged roles");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRoleById(int id)
        {
            try
            {
                var role = await _roleService.GetByIdAsync(id.ToString());
                if (role == null)
                {
                    return NotFound($"Role with ID {id} not found");
                }

                return Ok(role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting role by id: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Role>> CreateRole([FromBody] Role role)
        {
            try
            {
                if (role == null)
                {
                    return BadRequest("Role cannot be null");
                }

                var createdRole = await _roleService.AddAsync(role);
                return CreatedAtAction(nameof(GetRoleById), new { id = createdRole.RoleID }, createdRole);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating role");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Role>> UpdateRole(int id, [FromBody] Role role)
        {
            try
            {
                if (role == null)
                {
                    return BadRequest("Role cannot be null");
                }

                if (id != role.RoleID)
                {
                    return BadRequest("Role ID mismatch");
                }

                var existingRole = await _roleService.GetByIdAsync(id.ToString());
                if (existingRole == null)
                {
                    return NotFound($"Role with ID {id} not found");
                }

                var updatedRole = await _roleService.UpdateAsync(role);
                return Ok(updatedRole);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating role with id: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole(int id)
        {
            try
            {
                var result = await _roleService.DeleteByIdAsync(id.ToString());
                if (!result)
                {
                    return NotFound($"Role with ID {id} not found");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting role with id: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}