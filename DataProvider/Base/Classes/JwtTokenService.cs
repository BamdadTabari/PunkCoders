using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using DataProvider.EntityFramework.Entities.Identity;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using DataProvider.Certain.Configs;

public class JwtTokenService
{
    private readonly SecurityTokenConfig _configuration;

    public JwtTokenService(SecurityTokenConfig configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user, IEnumerable<string> roles)
    {
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, user.Username),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new ("userId", user.Id.ToString())
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var token = new JwtSecurityToken(
            issuer: _configuration.Issuer,
            audience: _configuration.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_configuration.AccessTokenLifetime.TotalMinutes),
            signingCredentials: creds
            
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
