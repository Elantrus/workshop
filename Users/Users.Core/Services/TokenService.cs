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

    public string GenerateToken(Customer customerDb)
    {
        var secretKey = new SymmetricSecurityKey(jwtSecurity);
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokeOptions = new JwtSecurityToken(
            claims: new List<Claim>()
            {
                new Claim(ClaimTypes.Role, customerDb.Role),
                new Claim(ClaimTypes.Email, customerDb.Email),
                new Claim(ClaimTypes.NameIdentifier, customerDb.UserId.ToString())
            },
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: signinCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        return tokenString;
    }

    public string RefreshToken(Customer customer, string token)
    {
        throw new NotImplementedException();
    }
}