using System.Threading.Tasks;
using dotNetTesting.Payments.DataAccess;
using dotNetTesting.Payments.Model;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace dotNetTesting.Tests.Payments.DataAccess;

[TestSubject(typeof(EntitiesRepository))]
public class EntitiesRepositoryTest
{
    private PaymentsDbContext PaymentsDbContext() => new(
        new DbContextOptionsBuilder<PaymentsDbContext>()
            .UseNpgsql(
                "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres;")
            .Options
    );


    [Fact]
    public async Task GetEntity()
    {
        await using var ctx = PaymentsDbContext();
        await ctx.Database.MigrateAsync(); 
        var repo = new EntitiesRepository(ctx);
        var entity = await repo.GetEntity(new EntityId("0"));
        Assert.IsNull(entity);

    }
}