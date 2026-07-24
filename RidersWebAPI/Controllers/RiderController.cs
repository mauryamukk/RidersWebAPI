using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RidersWebAPI.IServices;
using RidersWebAPI.Models;

namespace RidersWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiderController : ControllerBase
    {
        private readonly IRiderService _riderService;

        public RiderController(IRiderService riderService)
        {
            _riderService = riderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var riders = await _riderService.GetAllAsync();
            return Ok(riders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rider = await _riderService.GetByIdAsync(id);

            if (rider == null)
                return NotFound();

            return Ok(rider);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Rider rider)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _riderService.CreateAsync(rider);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Rider rider)
        {
            if (id != rider.Id)
                return BadRequest("Rider ID mismatch.");

            var result = await _riderService.UpdateAsync(rider);

            if (!result)
                return NotFound();

            return Ok("Rider updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _riderService.DeleteAsync(id);

            if (!result)
                return NotFound();

            return Ok("Rider deleted successfully.");
        }
    }
}
