using System.Diagnostics;
using dotNetTesting.Payments.Domain.Model;
using dotNetTesting.Payments.Model;
using Microsoft.EntityFrameworkCore;

namespace dotNetTesting.Payments.DataAccess;

public class EntitiesRepository : IEntitiesRepository
{
    private readonly PaymentsDbContext _context;

    public EntitiesRepository(PaymentsDbContext context)
    {
        _context = context;
    }

    public async Task<Entity?> GetEntity(EntityId id)
    {
        var row = await _context.Entities.SingleOrDefaultAsync(p => p.Id == id.Value);
        if (row == null) return null;
        var types = row.Types.Split(",").Select(Enum.Parse<EntityType>).ToHashSet();
        return new Entity(id, row.Name, types);
    }
}