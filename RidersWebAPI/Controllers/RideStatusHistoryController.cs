using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RidersWebAPI.IServices;
using RidersWebAPI.Models;

namespace RidersWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RideStatusHistoryController : ControllerBase
    {
        private readonly IRideStatusHistoryService _rideStatusHistoryService;

        public RideStatusHistoryController(IRideStatusHistoryService rideStatusHistoryService)
        {
            _rideStatusHistoryService = rideStatusHistoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _rideStatusHistoryService.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _rideStatusHistoryService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("ride/{rideId:int}")]
        public async Task<IActionResult> GetByRideId(int rideId)
        {
            var result = await _rideStatusHistoryService.GetByRideIdAsync(rideId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RideStatusHistory history)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _rideStatusHistoryService.CreateAsync(history);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] RideStatusHistory history)
        {
            if (id != history.Id)
                return BadRequest("Ride Status History ID mismatch.");

            var updated = await _rideStatusHistoryService.UpdateAsync(history);

            if (!updated)
                return NotFound();

            return Ok("Ride status history updated successfully.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _rideStatusHistoryService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return Ok("Ride status history deleted successfully.");
        }
    }
}
