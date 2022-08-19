using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Core.Entities;
using Users.Core.Exceptions;
using Users.Core.Extensions;
using Users.Core.Services;
using Users.Infrastructure.Data;

namespace Users.Application.Features;

public class AuthenticateUser
{
    public class Command : IRequest<Result>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class Result
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
    
    public class Handler : IRequestHandler<Command,
        Result>
    {
        private readonly UsersDbContext _usersDbContext;
        private readonly ITokenService _tokenService;

        public Handler(UsersDbContext usersDbContext, ITokenService tokenService)
        {
            _usersDbContext = usersDbContext;
            _tokenService = tokenService;
        }

        public async Task<Result> Handle(Command request,
            CancellationToken cancellationToken)
        {
            if (request.Password is null) throw new InvalidPasswordException();

            if (request.Email is null) throw new InvalidEmailException();

            var passwordHash = request.Password.Hash();

            var userDb = await _usersDbContext.Users
                .Include(x => x.Role)
                .SingleOrDefaultAsync(x =>
                        x.Email != null &&
                        x.Email.Equals(request.Email, StringComparison.InvariantCultureIgnoreCase),
                    cancellationToken: cancellationToken);

            if (userDb is null) throw new InvalidEmailException();

            if (string.IsNullOrEmpty(userDb.Password) ||
                !userDb.Password.Equals(passwordHash, StringComparison.InvariantCultureIgnoreCase))
                throw new InvalidPasswordException();

            var generatedJwtToken = _tokenService.GenerateToken(userDb);
            var refreshToken = Guid.NewGuid();

            await SetCustomerRefreshAndSave(refreshToken, userDb);

            return new Result()
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
}
