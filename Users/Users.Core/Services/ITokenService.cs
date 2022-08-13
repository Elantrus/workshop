using Users.Core.Entities;

namespace Users.Core.Services;

public interface ITokenService
{
    string GenerateToken(Customer customerDb);
    string RefreshToken(Customer customer, string token);
}