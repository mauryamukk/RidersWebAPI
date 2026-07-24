using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RidersWebAPI.IServices;
using RidersWebAPI.Models;

namespace RidersWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _usersService.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _usersService.GetByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _usersService.GetByEmailAsync(email);

            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Users user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _usersService.CreateAsync(user);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Users user)
        {
            if (id != user.Id)
                return BadRequest("User ID mismatch.");

            var updated = await _usersService.UpdateAsync(user);

            if (!updated)
                return NotFound();

            return Ok("User updated successfully.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _usersService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return Ok("User deleted successfully.");
        }
    }
}
