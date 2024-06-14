using dotNetTesting.Payments.Model;

namespace dotNetTesting.Payments;

public record Entity(EntityId Id, string Name, HashSet<EntityType> Types)
{
    
};