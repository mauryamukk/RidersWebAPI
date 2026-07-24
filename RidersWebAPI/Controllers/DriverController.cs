using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RidersWebAPI.IServices;
using RidersWebAPI.Models;

namespace RidersWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _driverService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var driver = await _driverService.GetByIdAsync(id);

            if (driver == null)
                return NotFound();

            return Ok(driver);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Driver driver)
        {
            var result = await _driverService.AddAsync(driver);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Driver driver)
        {
            await _driverService.UpdateAsync(driver);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _driverService.DeleteAsync(id);

            return NoContent();
        }

        [HttpPost("{id}/online")]
        public async Task<IActionResult> GoOnline(int id)
        {
            await _driverService.GoOnlineAsync(id);

            return Ok("Driver Online");
        }

        [HttpPost("{id}/offline")]
        public async Task<IActionResult> GoOffline(int id)
        {
            await _driverService.GoOfflineAsync(id);

            return Ok("Driver Offline");
        }

        [HttpPut("{id}/location")]
        public async Task<IActionResult> UpdateLocation(int id, decimal latitude, decimal longitude)
        {
            await _driverService.UpdateLocationAsync(id, latitude, longitude);

            return Ok("Location Updated");
        }
    }
}
