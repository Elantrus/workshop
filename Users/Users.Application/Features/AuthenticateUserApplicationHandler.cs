using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Contracts;
using Users.Core.Entities;
using Users.Core.Exceptions;
using Users.Core.Extensions;
using Users.Core.Services;
using Users.Infrastructure.Data;

namespace Users.Application.Features;

public class AuthenticateUserApplicationHandler : IRequestHandler<AuthenticateUser.AuthenticateUserCommand, AuthenticateUser.AuthenticateUserResult>
{
    private readonly UsersDbContext _usersDbContext;
    private readonly ITokenService _tokenService;
    public AuthenticateUserApplicationHandler(UsersDbContext usersDbContext, ITokenService tokenService)
    {
        _usersDbContext = usersDbContext;
        _tokenService = tokenService;
    }
    public async Task<AuthenticateUser.AuthenticateUserResult> Handle(AuthenticateUser.AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Password is null) throw new InvalidPasswordException();
        
        if (request.Email is null) throw new InvalidEmailException();
        
        var passwordHash = request.Password.Hash();

        var userDb = await _usersDbContext.Users.SingleOrDefaultAsync(x =>
            x.Email.Equals(request.Email, StringComparison.InvariantCultureIgnoreCase), cancellationToken: cancellationToken);

        if (userDb is null) throw new InvalidEmailException();

        if (!userDb.Password.Equals(passwordHash, StringComparison.InvariantCultureIgnoreCase))
            throw new InvalidPasswordException();
        
        var generatedJwtToken = _tokenService.GenerateToken(userDb);
        var refreshToken = Guid.NewGuid();

        await SetCustomerRefreshAndSave(refreshToken, userDb);
        
        return new AuthenticateUser.AuthenticateUserResult()
        {
            Token = generatedJwtToken,
            RefreshToken = userDb.RefreshToken.ToString()
        };
    }

    private async Task SetCustomerRefreshAndSave(Guid refreshToken, User userDb)
    {
        userDb.RefreshToken = refreshToken;
        await _usersDbContext.SaveChangesAsync();
    }
}