using IdentityService.API.Domain.Repositories;
using IdentityService.API.DTO;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityService.API.Persistence.Repositories;

public class JwtRepository(IOptions<JwtOption> _jwtOption) : IJwtRepository
{
    public AuthenticationResponseDto GenerateToken(AuthenticationRequestDto options)
    {
        var tokenExpirationTimeStamp = DateTime.Now.AddMinutes(_jwtOption.Value.ExpirationInMinute);

        var roleClaims = new List<Claim>();
        foreach (var role in options.Roles)
        {
            roleClaims.Add(new Claim(ClaimTypes.Role, role));
        }
        var claims = new[]
        {
                    new Claim(JwtRegisteredClaimNames.Sub,options.Id),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.GivenName,options.Name)
            }.Union(roleClaims);

        var seurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.Value.Key));
        var signingCredential = new SigningCredentials(seurityKey, SecurityAlgorithms.HmacSha256Signature);
        var jwtSecurityToken = new JwtSecurityToken(
        issuer: _jwtOption.Value.Issuer,
        audience: _jwtOption.Value.Audience,
        claims: claims,
        expires: tokenExpirationTimeStamp,
        signingCredentials: signingCredential);

        string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        int expireIn = (int)TimeSpan.FromMinutes(_jwtOption.Value.ExpirationInMinute).TotalSeconds;

        return new AuthenticationResponseDto(options.Username, token, expireIn);
    }
}
