namespace dotNetTesting.Payments.Model;

public record Payment(PaymentId Id, MonetaryAmount Amount, EntityId Payer, EntityId Payee);