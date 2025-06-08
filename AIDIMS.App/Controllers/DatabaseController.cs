using AIDIMS.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIDIMS.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DatabaseController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DatabaseController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("connection-test")]
    public async Task<IActionResult> TestConnection()
    {
        try
        {
            // Kiểm tra kết nối cơ sở dữ liệu
            bool canConnect = await _context.Database.CanConnectAsync();
            
            if (canConnect)
            {
                return Ok(new { status = "Success", message = "Kết nối đến PostgreSQL thành công!" });
            }
            else
            {
                return BadRequest(new { status = "Failed", message = "Không thể kết nối đến PostgreSQL." });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { status = "Error", message = $"Lỗi kết nối: {ex.Message}" });
        }
    }
} 