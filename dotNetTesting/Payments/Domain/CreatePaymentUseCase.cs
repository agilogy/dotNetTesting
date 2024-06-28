using dotNetTesting.Payments.Model;
using dotNetTesting.Services;

namespace dotNetTesting.Payments;



public class CreatePaymentUseCase : ICreatePaymentUseCase
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly IPaymentsRepository _paymentsRepository;
    private readonly IEntitiesRepository _entitiesRepository;

    public CreatePaymentUseCase(
        IGuidGenerator guidGenerator,
        IEntitiesRepository entitiesRepository,
        IPaymentsRepository paymentsRepository 
    )
    {
        _guidGenerator = guidGenerator;
        _paymentsRepository = paymentsRepository;
        _entitiesRepository = entitiesRepository;
    }

    public async Task<PaymentId> Invoke(CreatePaymentRequest request)
    {
        var payer = await _entitiesRepository.GetEntity(request.Payer);
        if (payer == null) throw new PayerDoesNotExist(request.Payer);
        if (!payer.Types.Contains(EntityType.Payer)) throw new NotAPayer(request.Payer);

        var payee = await _entitiesRepository.GetEntity(request.Payee);
        if (payee == null) throw new PayeeDoesNotExist(request.Payee);
        if (!payee.Types.Contains(EntityType.Payee)) throw new NotAPayee(request.Payee);

        var payment = new Payment(new PaymentId(_guidGenerator.GenerateGuid()), request.Amount, payer.Id, payee.Id);

        await _paymentsRepository.Add(payment);
        return payment.Id;
    }
}



public record CreatePaymentRequest(MonetaryAmount Amount, EntityId Payer, EntityId Payee);

public class PayerDoesNotExist : Exception
{
    public readonly EntityId Payer;

    public PayerDoesNotExist(EntityId payer)
    {
        Payer = payer;
    }
}

public class NotAPayer : Exception
{
    public readonly EntityId Payer;

    public NotAPayer(EntityId payer)
    {
        Payer = payer;
    }
}

public class PayeeDoesNotExist : Exception
{
    public readonly EntityId Payee;

    public PayeeDoesNotExist(EntityId payee)
    {
        Payee = payee;
    }
}

public class NotAPayee : Exception
{
    public readonly EntityId Payee;

    public NotAPayee(EntityId payee)
    {
        Payee = payee;
    }
}