using dotNetTesting.Payments;

namespace PaymentsApi;

public static class Endpoints
{
    public static void UseEndpoints(this WebApplication webApplication)
    {
        webApplication.MapPost("/payments", async (CreatePaymentRequest request, ICreatePaymentUseCase useCase) =>
        {
            var res = await useCase.Invoke(request);
            return Results.Created($"/payments/{res.Value}", res.Value);
        });

        webApplication.MapGet("/", () => Results.Ok("Hello world"));
    }
}