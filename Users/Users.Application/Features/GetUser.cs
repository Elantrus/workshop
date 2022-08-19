using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Core.Exceptions;
using Users.Infrastructure.Data;

namespace Users.Application.Features;

public class GetUser
{
    public class GetUserCommand : IRequest<Result>
    {
        public long UserId { get; set; }
    }
    public class Result
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
    
    public class
        Handler : IRequestHandler<GetUserCommand, Result>
    {
        private readonly UsersDbContext _usersDbContext;

        public Handler(UsersDbContext usersDbContext)
        {
            _usersDbContext = usersDbContext;
        }

        public async Task<Result> Handle(GetUserCommand request,
            CancellationToken cancellationToken)
        {
            var customerDb =
                await _usersDbContext.Users.SingleOrDefaultAsync(x => x.UserId == request.UserId,
                    cancellationToken: cancellationToken) ?? throw new UserNotFoundException();

            return customerDb.Adapt<Result>();
        }
    }
}
