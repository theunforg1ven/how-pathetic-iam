using HPIAM.Domain.Entities;

namespace HPIAM.Core.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
