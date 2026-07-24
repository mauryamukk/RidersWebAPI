using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RidersWebAPI.IServices;
using RidersWebAPI.Models;

namespace RidersWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vehicles = await _vehicleService.GetAllAsync();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);

            if (vehicle == null)
                return NotFound();

            return Ok(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _vehicleService.CreateAsync(vehicle);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Vehicle vehicle)
        {
            if (id != vehicle.Id)
                return BadRequest("Vehicle ID mismatch.");

            var result = await _vehicleService.UpdateAsync(vehicle);

            if (!result)
                return NotFound();

            return Ok("Vehicle updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _vehicleService.DeleteAsync(id);

            if (!result)
                return NotFound();

            return Ok("Vehicle deleted successfully.");
        }
    }
}
