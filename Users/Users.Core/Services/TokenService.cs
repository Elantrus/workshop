using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Users.Core.Entities;

namespace Users.Core.Services;

public class TokenService : ITokenService
{
    private readonly byte[] jwtSecurity;

    public TokenService(string jwtPassword)
    {
        jwtSecurity = Encoding.UTF8.GetBytes(jwtPassword);
    }

    public string GenerateToken(User userDb)
    {
        var secretKey = new SymmetricSecurityKey(jwtSecurity);
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokeOptions = new JwtSecurityToken(
            claims: new List<Claim>()
            {
                new Claim(ClaimTypes.Role, userDb.Role.Name),
                new Claim(ClaimTypes.Email, userDb.Email),
                new Claim(ClaimTypes.NameIdentifier, userDb.UserId.ToString())
            },
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: signinCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        return tokenString;
    }
}