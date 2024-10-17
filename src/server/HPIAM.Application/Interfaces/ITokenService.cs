using HPIAM.Domain.Entities;

namespace HPIAM.Application.Interfaces;
public interface ITokenService
{
    string CreateToken(AppUser user);
}
