using Mapster;
using MediatR;
using Users.Contracts;
using Users.Core.Entities;
using Users.Core.Exceptions;
using Users.Infrastructure.Data;

namespace Users.Application.Features;

public class CreateUserApplicationHandler : IRequestHandler<CreateUser.Command, CreateUser.Result>
{
    private readonly UsersDbContext _usersDbContext;
    public CreateUserApplicationHandler(UsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }
    public async Task<CreateUser.Result> Handle(CreateUser.Command request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException();

        var customerAlreadyExist = _usersDbContext.Customers.SingleOrDefault(x =>
            x.Email.Equals(request.Email, StringComparison.InvariantCultureIgnoreCase));

        if (customerAlreadyExist is not null) throw new EmailAlreadyExistsException();
        
        var customerDb = new Customer(request.Email, request.Name, request.Password);

        _usersDbContext.Customers.Add(customerDb);
        
        await _usersDbContext.SaveChangesAsync(cancellationToken);

        return customerDb.Adapt<CreateUser.Result>();
    }
}