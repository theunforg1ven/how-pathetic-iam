using HPIAM.Core.DTOs;
using HPIAM.Core.Interfaces;
using HPIAM.Domain.Entities;

namespace HPIAM.Core.Extensions;

public static class AppUserExtensions
{
    public static UserDto ToDto(this AppUser user, ITokenService tokenService)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            DisplayName = user.DisplayName,
            Token = tokenService.CreateToken(user)
        };
    }
}
