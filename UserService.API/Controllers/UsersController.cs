using Microsoft.AspNetCore.Mvc;
using UserService.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using UserService.Domain.Entities;
using UserService.Application.Dtos;
using System.Net.Http.Headers;


namespace UserService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUserApplicationService userService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        public IActionResult GetAllUsers()
        {
            var users = userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var user = await userService.CreateUserAsync(request);
            if (user == null)
            {
                return BadRequest("User creation failed");
            }
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest request)
        {
            await userService.UpdateUserAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}