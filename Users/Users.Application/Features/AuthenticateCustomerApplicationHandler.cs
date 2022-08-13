using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Contracts;
using Users.Core.Entities;
using Users.Core.Exceptions;
using Users.Core.Extensions;
using Users.Core.Services;
using Users.Infrastructure.Data;

namespace Users.Application.Features;

public class AuthenticateCustomerApplicationHandler : IRequestHandler<AuthenticateUser.Command, AuthenticateUser.Result>
{
    private readonly UsersDbContext _usersDbContext;
    private readonly ITokenService _tokenService;
    public AuthenticateCustomerApplicationHandler(UsersDbContext usersDbContext, ITokenService tokenService)
    {
        _usersDbContext = usersDbContext;
        _tokenService = tokenService;
    }
    public async Task<AuthenticateUser.Result> Handle(AuthenticateUser.Command request, CancellationToken cancellationToken)
    {
        if (request.Password is null) throw new InvalidPasswordException();
        
        if (request.Email is null) throw new InvalidEmailException();
        
        var passwordHash = request.Password.Hash();

        var customerDb = await _usersDbContext.Customers.SingleOrDefaultAsync(x =>
            x.Email.Equals(request.Email, StringComparison.InvariantCultureIgnoreCase), cancellationToken: cancellationToken);

        if (customerDb is null) throw new InvalidEmailException();

        if (!customerDb.Password.Equals(passwordHash, StringComparison.InvariantCultureIgnoreCase))
            throw new InvalidPasswordException();
        
        var generatedJwtToken = _tokenService.GenerateToken(customerDb);
        var refreshToken = Guid.NewGuid();

        await SetCustomerRefreshAndSave(refreshToken, customerDb);
        
        return new AuthenticateUser.Result()
        {
            Token = generatedJwtToken,
            RefreshToken = customerDb.RefreshToken.ToString()
        };
    }

    private async Task SetCustomerRefreshAndSave(Guid refreshToken, Customer customerDb)
    {
        customerDb.RefreshToken = refreshToken;
        await _usersDbContext.SaveChangesAsync();
    }
}