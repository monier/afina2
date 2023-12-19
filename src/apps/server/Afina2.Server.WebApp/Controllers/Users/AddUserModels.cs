using System.ComponentModel.DataAnnotations;

namespace Afina2.Server.WebApp.Controllers.Users;

public class AddUserRequest
{
    [Required]
    public string Username { get; set; } = null!;
}

public class AddUserResponse
{
    public string UserId { get; set; } = null!;
    public string Username { get; set; } = null!;
}