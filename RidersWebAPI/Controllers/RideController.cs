using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RidersWebAPI.IServices;
using RidersWebAPI.Models;

namespace RidersWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RideController : ControllerBase
    {
        private readonly IRideService _rideService;

        public RideController(IRideService rideService)
        {
            _rideService = rideService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rides = await _rideService.GetAllAsync();
            return Ok(rides);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ride = await _rideService.GetByIdAsync(id);

            if (ride == null)
                return NotFound();

            return Ok(ride);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ride ride)
        {
            var result = await _rideService.CreateAsync(ride);

            return CreatedAtAction(nameof(GetById), new { id = result.RiderId }, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Ride ride)
        {
            var result = await _rideService.UpdateAsync(ride);

            if (!result)
                return NotFound();

            return Ok("Ride updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _rideService.DeleteAsync(id);

            if (!result)
                return NotFound();

            return Ok("Ride deleted successfully.");
        }

        [HttpPost("{id}/start")]
        public async Task<IActionResult> StartRide(int id)
        {
            var result = await _rideService.StartRideAsync(id);

            if (!result)
                return NotFound();

            return Ok("Ride started successfully.");
        }

        [HttpPost("{id}/complete")]
        public async Task<IActionResult> CompleteRide(int id)
        {
            var result = await _rideService.CompleteRideAsync(id);

            if (!result)
                return NotFound();

            return Ok("Ride completed successfully.");
        }
    }
}
