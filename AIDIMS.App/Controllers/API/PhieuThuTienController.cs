using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AIDIMS.App.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhieuThuTienController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new { message = "Lấy danh sách phiếu thu tiền thành công" });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return Ok(new { message = $"Lấy thông tin phiếu thu tiền có ID: {id} thành công" });
        }

        [HttpPost]
        public IActionResult Create(CreatePhieuThuTienDto createPhieuThuTienDto)
        {
            return Ok(new { message = "Tạo phiếu thu tiền mới thành công" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, UpdatePhieuThuTienDto updatePhieuThuTienDto)
        {
            return Ok(new { message = $"Cập nhật thông tin phiếu thu tiền có ID: {id} thành công" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return Ok(new { message = $"Xóa phiếu thu tiền có ID: {id} thành công" });
        }
    }
} 