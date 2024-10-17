using HPIAM.Application.Interfaces;
using HPIAM.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HPIAM.Application.Services;
public class TokenService : ITokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config)
    {
        _config = config;
    }

    public string CreateToken(AppUser user)
    {
        // get token key from appsettings.json
        var tokenKey = _config["TokenKey"] ?? throw new Exception("Cannot access token key from appsettings json");
        if (tokenKey.Length < 64) 
            throw new Exception("Token key has to be at least 64 chars long");

        // generate symmetric key
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        
        // create user claims
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserName)
        };

        // create credentials with symmetric key and algorithm
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        // create token descriptor with claims, exrire date and credentials
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds,
        };

        // init a new token handler instance & create token using token handler
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        // serialize security token into JWT
        return tokenHandler.WriteToken(token);
    }
}
