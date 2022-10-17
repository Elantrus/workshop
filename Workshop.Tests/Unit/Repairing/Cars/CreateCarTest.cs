using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Repairing.Application.Features;
using Repairing.Application.Features.Cars;

namespace Workshop.Tests.Unit.Repairing;

public class CreateCarTest
{
    [Test]
    public async Task CarCreate_ShouldCreateCar()
    {
        var dbContext = InMemoryDatabase.CreateRepairingDb();

        var handler = new CreateCar.Handler(dbContext);
        var command = new CreateCar.CreateCarCommand()
        {
            LicensePlate = "CRT-0000"
        };
        
        var sendResult = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(sendResult);
    }
}