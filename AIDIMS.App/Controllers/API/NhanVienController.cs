using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AIDIMS.App.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class NhanVienController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new { message = "Lấy danh sách nhân viên thành công" });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return Ok(new { message = $"Lấy thông tin nhân viên có ID: {id} thành công" });
        }

        [HttpPost]
        public IActionResult Create(CreateNhanVienDto createNhanVienDto)
        {
            return Ok(new { message = "Tạo nhân viên mới thành công" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, UpdateNhanVienDto updateNhanVienDto)
        {
            return Ok(new { message = $"Cập nhật thông tin nhân viên có ID: {id} thành công" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return Ok(new { message = $"Xóa nhân viên có ID: {id} thành công" });
        }
    }
} 