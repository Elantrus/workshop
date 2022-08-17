using Users.Core.Entities;

namespace Users.Core.Services;

public interface ITokenService
{
    string GenerateToken(User userDb);
}