using System.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Core.Exceptions;
using Users.Core.Services;
using Users.Infrastructure.Data;

namespace Users.Application.Features;

public class RefreshUser
{
    public class Command : IRequest<Result>
    {
        public string? RefreshToken { get; set; }
    }

    public class Result
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly UsersDbContext _usersDbContext;
        private readonly ITokenService _tokenService;

        public Handler( UsersDbContext usersDbContext, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _usersDbContext = usersDbContext;
        }

        public async Task<Result> Handle(Command request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request?.RefreshToken)) throw new NoNullAllowedException();
        
            var refreshTokenGuid = Guid.Parse(request.RefreshToken);
            var customerDb =
                await _usersDbContext.Customers.SingleOrDefaultAsync(x => x.RefreshToken == refreshTokenGuid,
                    cancellationToken: cancellationToken) ?? throw new RefreshTokenIsNotValidException();

            var generatedToken = _tokenService.GenerateToken(customerDb);

            return new Result
            {
                Token = generatedToken,
                RefreshToken = request.RefreshToken
            };
        }
    }
}
