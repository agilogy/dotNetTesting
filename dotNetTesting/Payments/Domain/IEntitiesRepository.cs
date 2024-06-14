using dotNetTesting.Payments.Domain.Model;
using dotNetTesting.Payments.Model;

namespace dotNetTesting.Payments;

public interface IEntitiesRepository
{
    public Task<Entity?> GetEntity(EntityId requestPayer);
}