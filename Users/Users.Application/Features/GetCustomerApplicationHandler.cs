using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Contracts;
using Users.Core.Exceptions;
using Users.Infrastructure.Data;

namespace Users.Application.Features;

public class
    GetCustomerApplicationHandler : IRequestHandler<GetCustomer.GetCustomerCommand, GetCustomer.GetCustomerResult>
{
    private readonly UsersDbContext _usersDbContext;

    public GetCustomerApplicationHandler(UsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }

    public async Task<GetCustomer.GetCustomerResult> Handle(GetCustomer.GetCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var customerDb =
            await _usersDbContext.Customers.SingleOrDefaultAsync(x => x.UserId == request.UserId,
                cancellationToken: cancellationToken) ?? throw new UserNotFoundException();

        return customerDb.Adapt<GetCustomer.GetCustomerResult>();
    }
}