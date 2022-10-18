using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Repairing.Application.Features.Cars;
using Repairing.Application.Features.Repairs;
using Repairing.Core.Exceptions;

namespace Workshop.Tests.Unit.Repairing.Repairs;

public class CreateRepairTest
{
    [Test]
    public void CreateRepair_ShouldThrowsCarNotExistsException()
    {
        var dbContext = InMemoryDatabase.CreateRepairingDb();

        var handler = new CreateRepair.Handler(dbContext);
        var command = new CreateRepair.CreateRepairCommand()
        {
            CarLicensePlate = "RPR-000",
            UserId = 1
        };

        async Task HandleCreateRepair() => await handler.Handle(command, CancellationToken.None);

        Assert.ThrowsAsync<CarNotExistsException>(HandleCreateRepair);
    }
    
    [Test]
    public async Task CreateRepair_ShouldReturnRepairId()
    {
        var dbContext = InMemoryDatabase.CreateRepairingDb();

        var licensePlate = "RPR-001";
        var createCarHandle = new CreateCar.Handler(dbContext);
        var createCarCommand = new CreateCar.CreateCarCommand
        {
            LicensePlate = licensePlate
        };
        await createCarHandle.Handle(createCarCommand, CancellationToken.None);

        var createRepairHandle = new CreateRepair.Handler(dbContext);
        var createRepairCommand = new CreateRepair.CreateRepairCommand()
        {
            CarLicensePlate = licensePlate,
            UserId = 1
        };

        var result = await createRepairHandle.Handle(createRepairCommand, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotZero(result.RepairId);
    }
}