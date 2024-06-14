using System.Collections.Generic;
using System.Threading.Tasks;
using dotNetTesting.Payments;
using dotNetTesting.Payments.Model;

namespace dotNetTesting.Tests.Payments;

public record InMemoryEntitiesRepository(List<Entity> Entities) : IEntitiesRepository
{
    public Task<Entity> GetEntity(EntityId id)
    {
        return Task.FromResult(Entities.Find(e => e.Id == id));
    }
}