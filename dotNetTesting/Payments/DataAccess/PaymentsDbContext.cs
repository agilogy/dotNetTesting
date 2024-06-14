using dotNetTesting.Payments.Domain.Model;
using dotNetTesting.Payments.Model;
using Microsoft.EntityFrameworkCore;

namespace dotNetTesting.Payments.DataAccess;

public class PaymentsDbContext : DbContext
{
    // TODO: Remove
    public PaymentsDbContext()
        : base(new DbContextOptionsBuilder<PaymentsDbContext>()
            .UseNpgsql(
                "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres;")
            .Options)
    {
    }

    
    public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options)
        : base(options)
    {
    }

    public DbSet<EntityRow> Entities => Set<EntityRow>();
    
    internal static async Task<TA> InContext<TA>(DbContextOptions<PaymentsDbContext> options, Func<PaymentsDbContext, TA> block)
    {
        await using var context = new PaymentsDbContext(options);
        var result = block(context);
        await context.SaveChangesAsync();
        return result;
    }

}