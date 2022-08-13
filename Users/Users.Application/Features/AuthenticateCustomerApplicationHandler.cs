using MediatR;
using Users.Contracts;
using Users.Core.Exceptions;
using Users.Core.Extensions;
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
        if (request.Password is null) throw new InvalidPasswordException();
        
        if (request.Email is null) throw new InvalidEmailException();
        
        var passwordHash = request.Password.Hash();

        var customerDb = _usersDbContext.Customers.SingleOrDefault(x =>
            x.Email.Equals(request.Email, StringComparison.InvariantCultureIgnoreCase));

        if (customerDb is null) throw new InvalidEmailException();

        if (!customerDb.Password.Equals(passwordHash, StringComparison.InvariantCultureIgnoreCase))
            throw new InvalidPasswordException();

        throw new NotImplementedException();
    }
}