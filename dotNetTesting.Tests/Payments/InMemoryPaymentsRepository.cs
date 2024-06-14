using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using dotNetTesting.Payments;
using dotNetTesting.Payments.Model;

namespace dotNetTesting.Tests.Payments;

public class InMemoryPaymentsRepository: IPaymentsRepository
{
    private List<Payment> _payments = new();
    
    public Task Add(Payment payment)
    {
        _payments.Add(payment);
        return Task.CompletedTask;
    }

    public ImmutableList<Payment> State => _payments.ToImmutableList();
}