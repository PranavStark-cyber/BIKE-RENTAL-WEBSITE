using BIKERENTALAPI.DTOs.RequestDTO;
using BIKERENTALAPI.IServies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BIKERENTALAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerServies _managerService;

        public ManagerController(IManagerServies managerService)
        {
            _managerService = managerService;
        }

        [HttpPost("AddBike")]
        public async Task<IActionResult> AddBike([FromForm] ManagerRequestDTO request)
        {
            var result = await _managerService.AddBikeAsync(request);
            return Ok(result);
        }

        [HttpPut("{bikeId}")]
        public async Task<IActionResult> EditBike(Guid bikeId, [FromForm] ManagerRequestDTO bikeRequest)
        {
            var updatedBike = await _managerService.EditBikeAsync(bikeId, bikeRequest);
            if (updatedBike == null) return NotFound();

            return Ok(updatedBike);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetBike(Guid id)
        {
            var result = await _managerService.GetBikeByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("AllBikes")]
        public async Task<IActionResult> GetAllBikes()
        {
            var result = await _managerService.GetAllBikesAsync();
            return Ok(result);
        }
        [HttpDelete("{bikeId}")]
        public async Task<IActionResult> DeleteBike(Guid bikeId)
        {
            var isDeleted = await _managerService.DeleteBikeAsync(bikeId);
            if (!isDeleted) return NotFound();

            return NoContent();
        }

    }
}
