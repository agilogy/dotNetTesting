using dotNetTesting.Payments.Model;

namespace dotNetTesting.Payments.Domain.Model;

public record Entity(EntityId Id, string Name, HashSet<EntityType> Types)
{
    
};