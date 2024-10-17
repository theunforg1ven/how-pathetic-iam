using System.ComponentModel.DataAnnotations;

namespace HPIAM.Application.DTOs.Auth;
public class LoginDto
{
    [Required]
    public required string Username { get; set; }

    [Required]
    public required string Password { get; set; }
}
