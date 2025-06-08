using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AIDIMS.App.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class HinhAnhDicomController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new { message = "Lấy danh sách hình ảnh DICOM thành công" });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return Ok(new { message = $"Lấy thông tin hình ảnh DICOM có ID: {id} thành công" });
        }

        [HttpPost]
        public IActionResult Create(CreateHinhAnhDicomDto createHinhAnhDicomDto)
        {
            return Ok(new { message = "Tạo hình ảnh DICOM mới thành công" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, UpdateHinhAnhDicomDto updateHinhAnhDicomDto)
        {
            return Ok(new { message = $"Cập nhật thông tin hình ảnh DICOM có ID: {id} thành công" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return Ok(new { message = $"Xóa hình ảnh DICOM có ID: {id} thành công" });
        }
    }
} 