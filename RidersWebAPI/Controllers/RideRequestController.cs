using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RidersWebAPI.IServices;
using RidersWebAPI.Models;

namespace RidersWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RideRequestController : ControllerBase
    {
        private readonly IRideRequestService _rideRequestService;

        public RideRequestController(IRideRequestService rideRequestService)
        {
            _rideRequestService = rideRequestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _rideRequestService.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var request = await _rideRequestService.GetByIdAsync(id);

            if (request == null)
                return NotFound();

            return Ok(request);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RideRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _rideRequestService.CreateAsync(request);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] RideRequest request)
        {
            if (id != request.Id)
                return BadRequest("Ride Request ID mismatch.");

            var updated = await _rideRequestService.UpdateAsync(request);

            if (!updated)
                return NotFound();

            return Ok("Ride request updated successfully.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _rideRequestService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return Ok("Ride request deleted successfully.");
        }
    }
}
