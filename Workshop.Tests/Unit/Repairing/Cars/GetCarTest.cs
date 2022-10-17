using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Repairing.Application.Features.Cars;

namespace Workshop.Tests.Unit.Repairing;

public class GetCarTest
{
    [Test]
    public async Task GetCar_ShouldReturnCarInfo()
    {
        var dbContext = InMemoryDatabase.CreateRepairingDb();

        var licensePlate = "GET-0000";
        var createCarHandler = new CreateCar.Handler(dbContext);
        var createCarCommand = new CreateCar.CreateCarCommand()
        {
            LicensePlate = licensePlate
        };
        
        var sendResult = await createCarHandler.Handle(createCarCommand, CancellationToken.None);

        var getCarHandler = new GetCar.Handler(dbContext);
        var getCarCommand = new GetCar.GetCarCommand()
        {
            LicensePlate = licensePlate
        };

        var sendGetCarResult = await getCarHandler.Handle(getCarCommand, CancellationToken.None);
        
        Assert.NotNull(sendGetCarResult.LicensePlate);
    }
}