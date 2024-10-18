using System.ComponentModel.DataAnnotations;

namespace HPIAM.Application.DTOs.UserData;

public class UserDto
{
    [Required]
    public required string Username { get; set; }
    
    [Required]
    public required string AccessToken { get; set; }
}
