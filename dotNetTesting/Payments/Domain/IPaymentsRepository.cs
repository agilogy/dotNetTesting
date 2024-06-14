using dotNetTesting.Payments.Model;

namespace dotNetTesting.Payments;

public interface IPaymentsRepository
{
    Task Add(Payment payment);
}