using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AIDIMS.App.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class DichVuController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new { message = "Lấy danh sách dịch vụ thành công" });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return Ok(new { message = $"Lấy thông tin dịch vụ có ID: {id} thành công" });
        }

        [HttpPost]
        public IActionResult Create(CreateDichVuDto createDichVuDto)
        {
            return Ok(new { message = "Tạo dịch vụ mới thành công" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, UpdateDichVuDto updateDichVuDto)
        {
            return Ok(new { message = $"Cập nhật thông tin dịch vụ có ID: {id} thành công" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return Ok(new { message = $"Xóa dịch vụ có ID: {id} thành công" });
        }
    }
} 