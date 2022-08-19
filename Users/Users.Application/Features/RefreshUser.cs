using System.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Core.Exceptions;
using Users.Core.Services;
using Users.Infrastructure.Data;

namespace Users.Application.Features;

public class RefreshUser
{
    public class RefreshUserCommand : IRequest<RefreshUserResult>
    {
        public string? RefreshToken { get; set; }
    }

    public class RefreshUserResult
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
    
    public class Handler : IRequestHandler<RefreshUserCommand, RefreshUserResult>
    {
        private readonly UsersDbContext _usersDbContext;
        private readonly ITokenService _tokenService;

        public Handler( UsersDbContext usersDbContext, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _usersDbContext = usersDbContext;
        }

        public async Task<RefreshUserResult> Handle(RefreshUserCommand request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request?.RefreshToken)) throw new NoNullAllowedException();
        
            var refreshTokenGuid = Guid.Parse(request.RefreshToken);
            var userDb =
                await _usersDbContext.Users
                    .Include(x => x.Role)
                    .SingleOrDefaultAsync(x => x.RefreshToken == refreshTokenGuid,
                    cancellationToken: cancellationToken) ?? throw new RefreshTokenIsNotValidException();

            var generatedToken = _tokenService.GenerateToken(userDb);

            return new RefreshUserResult
            {
                Token = generatedToken,
                RefreshToken = request.RefreshToken
            };
        }
    }
}
