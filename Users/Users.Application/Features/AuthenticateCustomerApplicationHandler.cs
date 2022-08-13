using MediatR;
using Users.Contracts;
using Users.Infrastructure.Data;

namespace Users.Application.Features;

public class AuthenticateCustomerApplicationHandler : IRequestHandler<AuthenticateUser.Command, AuthenticateUser.Result>
{
    private readonly UsersDbContext _usersDbContext;
    public AuthenticateCustomerApplicationHandler(UsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }
    public Task<AuthenticateUser.Result> Handle(AuthenticateUser.Command request, CancellationToken cancellationToken)
    {
        if(_usersDbContext.Customer)
    }
}