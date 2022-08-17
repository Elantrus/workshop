using System.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Contracts;
using Users.Core.Exceptions;
using Users.Core.Services;
using Users.Infrastructure.Data;

namespace Users.Application.Features;

public class RefreshUserApplicationHandler : IRequestHandler<RefreshUser.RefreshUserCommand, RefreshUser.RefreshUserResult>
{
    private readonly UsersDbContext _usersDbContext;
    private readonly ITokenService _tokenService;

    public RefreshUserApplicationHandler( UsersDbContext usersDbContext, ITokenService tokenService)
    {
        _tokenService = tokenService;
        _usersDbContext = usersDbContext;
    }

    public async Task<RefreshUser.RefreshUserResult> Handle(RefreshUser.RefreshUserCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request?.RefreshToken)) throw new NoNullAllowedException();
        
        var refreshTokenGuid = Guid.Parse(request.RefreshToken);
        var customerDb =
            await _usersDbContext.Customers.SingleOrDefaultAsync(x => x.RefreshToken == refreshTokenGuid,
                cancellationToken: cancellationToken) ?? throw new RefreshTokenIsNotValidException();

        var generatedToken = _tokenService.GenerateToken(customerDb);

        return new RefreshUser.RefreshUserResult
        {
            Token = generatedToken,
            RefreshToken = request.RefreshToken
        };
    }
}