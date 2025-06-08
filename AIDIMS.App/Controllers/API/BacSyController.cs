using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.DTOs.Response;
using AIDIMS.Core.Enums;
using AIDIMS.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AIDIMS.App.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class BacSyController : ControllerBase
    {
        private readonly IBacSyService _bacSyService;

        public BacSyController(IBacSyService bacSyService)
        {
            _bacSyService = bacSyService;
        }

        /// <summary>
        /// Lấy danh sách tất cả bác sĩ
        /// </summary>
        /// <returns>Danh sách bác sĩ</returns>
        [HttpGet]
        [ProducesResponseType(typeof(BacSyListResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BacSyListResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _bacSyService.GetAllAsync();
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Lấy thông tin bác sĩ theo ID
        /// </summary>
        /// <param name="id">ID của bác sĩ</param>
        /// <returns>Thông tin chi tiết bác sĩ</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BacSyResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BacSyResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BacSyResponseDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _bacSyService.GetByIdAsync(id);
            
            if (!response.Success)
            {
                if (response.Message.Contains("Không tìm thấy"))
                    return NotFound(response);
                
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Tạo mới bác sĩ
        /// </summary>
        /// <param name="createBacSyDto">Thông tin bác sĩ cần tạo</param>
        /// <returns>Bác sĩ đã được tạo</returns>
        [HttpPost]
        [ProducesResponseType(typeof(BacSyResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BacSyResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateBacSyDto createBacSyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _bacSyService.CreateAsync(createBacSyDto);
            
            if (!response.Success)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);
        }

        /// <summary>
        /// Cập nhật thông tin bác sĩ
        /// </summary>
        /// <param name="id">ID của bác sĩ</param>
        /// <param name="updateBacSyDto">Thông tin cập nhật</param>
        /// <returns>Bác sĩ đã được cập nhật</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BacSyResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BacSyResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BacSyResponseDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateBacSyDto updateBacSyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _bacSyService.UpdateAsync(id, updateBacSyDto);
            
            if (!response.Success)
            {
                if (response.Message.Contains("Không tìm thấy"))
                    return NotFound(response);
                
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Xóa bác sĩ
        /// </summary>
        /// <param name="id">ID của bác sĩ</param>
        /// <returns>Kết quả xóa</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _bacSyService.DeleteAsync(id);
            
            if (!result)
                return NotFound(new { success = false, message = $"Không tìm thấy bác sĩ có ID: {id}" });

            return NoContent();
        }

        /// <summary>
        /// Tìm bác sĩ theo chuyên môn
        /// </summary>
        /// <param name="chuyenMon">Chuyên môn</param>
        /// <returns>Danh sách bác sĩ có chuyên môn tương ứng</returns>
        [HttpGet("chuyen-mon/{chuyenMon}")]
        [ProducesResponseType(typeof(BacSyListResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BacSyListResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FindByChuyenMon(ChuyenMon chuyenMon)
        {
            var response = await _bacSyService.FindByChuyenMonAsync(chuyenMon);
            
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Tìm bác sĩ theo chức vụ
        /// </summary>
        /// <param name="chucVu">Chức vụ</param>
        /// <returns>Danh sách bác sĩ có chức vụ tương ứng</returns>
        [HttpGet("chuc-vu/{chucVu}")]
        [ProducesResponseType(typeof(BacSyListResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BacSyListResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FindByChucVu(ChucVu chucVu)
        {
            var response = await _bacSyService.FindByChucVuAsync(chucVu);
            
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Lấy danh sách bác sĩ có phân trang
        /// </summary>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageSize">Số lượng mỗi trang</param>
        /// <returns>Danh sách bác sĩ được phân trang</returns>
        [HttpGet("paged")]
        [ProducesResponseType(typeof(BacSyListResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BacSyListResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPaged(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
                return BadRequest(new { success = false, message = "Số trang và kích thước trang phải lớn hơn 0" });

            var response = await _bacSyService.GetPagedListAsync(pageNumber, pageSize);
            
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
} 