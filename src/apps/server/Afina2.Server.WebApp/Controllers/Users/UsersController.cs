using Afina2.Server.WebApp.Infrastructure.Web;
using Microsoft.AspNetCore.Mvc;

namespace Afina2.Server.WebApp.Controllers.Users;
public class UsersController : ApiControllerBase
{

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] AddUserRequest user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        AddUserResponse response = new AddUserResponse
        {
            UserId = "1",
            Username = user.Username
        };
        return CreatedAtAction(nameof(GetUser), new { userId = response.UserId }, response);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser([FromRoute] string userId)
    {
        GetUserResponse response = new GetUserResponse
        {
            UserId = userId,
            Username = "createdUser"
        };
        return Ok(response);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser([FromRoute] string userId)
    {
        return NoContent();
    }
}

