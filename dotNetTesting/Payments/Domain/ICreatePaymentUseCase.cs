using dotNetTesting.Payments.Model;

namespace dotNetTesting.Payments;

public interface ICreatePaymentUseCase
{
    public Task<PaymentId> Invoke(CreatePaymentRequest request);
}