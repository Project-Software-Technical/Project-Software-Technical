using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AIDIMS.App.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class KetQuaChanDoanController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new { message = "Lấy danh sách kết quả chẩn đoán thành công" });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return Ok(new { message = $"Lấy thông tin kết quả chẩn đoán có ID: {id} thành công" });
        }

        [HttpPost]
        public IActionResult Create(CreateKetQuaChanDoanDto createKetQuaChanDoanDto)
        {
            return Ok(new { message = "Tạo kết quả chẩn đoán mới thành công" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, UpdateKetQuaChanDoanDto updateKetQuaChanDoanDto)
        {
            return Ok(new { message = $"Cập nhật thông tin kết quả chẩn đoán có ID: {id} thành công" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return Ok(new { message = $"Xóa kết quả chẩn đoán có ID: {id} thành công" });
        }
    }
} 