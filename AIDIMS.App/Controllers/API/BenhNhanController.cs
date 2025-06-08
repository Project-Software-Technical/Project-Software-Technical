using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AIDIMS.App.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class BenhNhanController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new { message = "Lấy danh sách bệnh nhân thành công" });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return Ok(new { message = $"Lấy thông tin bệnh nhân có ID: {id} thành công" });
        }

        [HttpPost]
        public IActionResult Create(CreateBenhNhanDto createBenhNhanDto)
        {
            return Ok(new { message = "Tạo bệnh nhân mới thành công" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, UpdateBenhNhanDto updateBenhNhanDto)
        {
            return Ok(new { message = $"Cập nhật thông tin bệnh nhân có ID: {id} thành công" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return Ok(new { message = $"Xóa bệnh nhân có ID: {id} thành công" });
        }
    }
} 