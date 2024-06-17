using dotNetTesting.Payments;
using PaymentsApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.AddDependencies();

var app = builder.Build();
app.UseHttpsRedirection();


app.MapPost("/payments", async (CreatePaymentRequest request, ICreatePaymentUseCase useCase) =>
{
    var res = await useCase.Invoke(request);
    return Results.Created($"/payments/{res.Value}", res.Value);
});

app.MapGet("/", () => Results.Ok("Hello world"));


app.Run();