using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RidersWebAPI.IServices;
using RidersWebAPI.Models;

namespace RidersWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverLocationController : ControllerBase
    {
        private readonly IDriverLocationService _driverLocationService;

        public DriverLocationController(IDriverLocationService driverLocationService)
        {
            _driverLocationService = driverLocationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _driverLocationService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _driverLocationService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("driver/{driverId}")]
        public async Task<IActionResult> GetByDriver(int driverId)
        {
            var result = await _driverLocationService.GetByDriverIdAsync(driverId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DriverLocation location)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _driverLocationService.CreateAsync(location);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] DriverLocation location)
        {
            if (id != location.Id)
                return BadRequest("Driver Location ID mismatch.");

            var updated = await _driverLocationService.UpdateAsync(location);

            if (!updated)
                return NotFound();

            return Ok("Driver location updated successfully.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _driverLocationService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return Ok("Driver location deleted successfully.");
        }
    }
}
