using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AIDIMS.App.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class YeuCauChupController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new { message = "Lấy danh sách yêu cầu chụp thành công" });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return Ok(new { message = $"Lấy thông tin yêu cầu chụp có ID: {id} thành công" });
        }

        [HttpPost]
        public IActionResult Create(CreateYeuCauChupDto createYeuCauChupDto)
        {
            return Ok(new { message = "Tạo yêu cầu chụp mới thành công" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, UpdateYeuCauChupDto updateYeuCauChupDto)
        {
            return Ok(new { message = $"Cập nhật thông tin yêu cầu chụp có ID: {id} thành công" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return Ok(new { message = $"Xóa yêu cầu chụp có ID: {id} thành công" });
        }
    }
} 