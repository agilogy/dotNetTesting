using dotNetTesting.Payments;
using JetBrains.Annotations;
using Xunit;

namespace dotNetTesting.Tests.Payments;

using System.Collections.Generic;
using System.Threading.Tasks;
using dotNetTesting.Payments;
using dotNetTesting.Payments.Model;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestSubject(typeof(CreatePaymentUseCase))]
public class CreatePaymentUseCaseTest
{
    private static readonly EntityId PayeeId = new("1");
    private static readonly EntityId PayerId = new("2");
    private static readonly EntityId PayerAndPayeeId = new("3");

    private static readonly List<string> PaymentIds = new() { "1", "2", "3" };

    private readonly InMemoryGuidGenerator _guidGenerator;
    private readonly InMemoryEntitiesRepository _entitiesRepository;
    private readonly InMemoryPaymentsRepository _paymentsRepository;
    private readonly CreatePaymentUseCase _useCase;

    private readonly List<Entity> _entities = new()
    {
        new(PayeeId, "Acme, Inc.", new HashSet<EntityType>() { EntityType.Payee }),
        new(PayerId, "Mars & co.", new HashSet<EntityType>() { EntityType.Payer }),
        new(PayerAndPayeeId, "Global Roles", new HashSet<EntityType>() { EntityType.Payer, EntityType.Payee }),
    };

    public CreatePaymentUseCaseTest()
    {
        _guidGenerator = new(new List<string> { "1", "2", "3" });
        _entitiesRepository = new(_entities);
        _paymentsRepository = new();
        _useCase = new CreatePaymentUseCase(this._guidGenerator, _entitiesRepository, _paymentsRepository);
    }


    [Fact]
    public async Task CreateAPayment()
    {
        var amount = new MonetaryAmount(2500, Currency.Eur);
        var res = await _useCase.Invoke(new(amount, PayerId, PayeeId));
        var expectedPaymentId = new PaymentId(PaymentIds[0]);
        var expectedPayment = new Payment(expectedPaymentId, amount, PayerId, PayeeId);
        Assert.AreEqual(expectedPaymentId, res);
        Assert.AreEqual(1, _paymentsRepository.State.Count);
        Assert.AreEqual(expectedPayment, _paymentsRepository.State[0]);
    }

    [Fact]
    public async Task CreateAPaymentThrowsIfPayerDoesNotExist()
    {
        var amount = new MonetaryAmount(2500, Currency.Eur);
        var nonExistingEntityId = new EntityId("notFound");
        var exception = await Assert.ThrowsExceptionAsync<PayerDoesNotExist>(() =>
        {
            return _useCase.Invoke(new(amount, nonExistingEntityId, PayeeId));
        });
        Assert.AreEqual(nonExistingEntityId, exception.Payer);
    }

    [Fact]
    public async Task CreateAPaymentThrowsIfPayerIsNotAPayer()
    {
        var amount = new MonetaryAmount(2500, Currency.Eur);
        var exception = await Assert.ThrowsExceptionAsync<NotAPayer>(() =>
        {
            return _useCase.Invoke(new(amount, PayeeId, PayerAndPayeeId));
        });
        Assert.AreEqual(PayeeId, exception.Payer);
    }

    [Fact]
    public async Task CreateAPaymentThrowsIfPayeeDoesNotExist()
    {
        var amount = new MonetaryAmount(2500, Currency.Eur);
        var nonExistingEntityId = new EntityId("notFound");
        var exception =
            await Assert.ThrowsExceptionAsync<PayeeDoesNotExist>(() =>
                _useCase.Invoke(new(amount, PayerId, nonExistingEntityId)));
        Assert.AreEqual(nonExistingEntityId, exception.Payee);
    }


    [Fact]
    public async Task CreateAPaymentThrowsIfPayeeIsNotAPayee()
    {
        var amount = new MonetaryAmount(2500, Currency.Eur);
        var exception =
            await Assert.ThrowsExceptionAsync<NotAPayee>(() =>
                _useCase.Invoke(new(amount, PayerAndPayeeId, PayerId)));
        Assert.AreEqual(PayerId, exception.Payee);
    }
}