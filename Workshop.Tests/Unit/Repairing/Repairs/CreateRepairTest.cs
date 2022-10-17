using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Repairing.Application.Features;
using Repairing.Application.Features.Repairs;

namespace Workshop.Tests.Unit.Repairing;

public class CreateRepairTest
{
    [Test]
    public async Task CreateRepair_ShouldReturnIdGreaterThanZero()
    {
        var dbContext = InMemoryDatabase.CreateRepairingDb();

        var handler = new CreateRepair.Handler(dbContext);
        var command = new CreateRepair.CreateRepairCommand()
        {
            
        };
        
        var sendResult = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(sendResult);
        Assert.NotZero(sendResult.RepairId);
    }
}