using dotNetTesting.Payments;
using dotNetTesting.Payments.DataAccess;
using dotNetTesting.Services;
using Microsoft.EntityFrameworkCore;

namespace PaymentsApi;

public static class DependencyInjection
{
    public static void AddDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContextPool<PaymentsDbContext>(options =>
        {
            options.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres;");
        });
        builder.Services.AddScoped<IEntitiesRepository, EntitiesRepository>();
        builder.Services.AddScoped<IPaymentsRepository, PaymentsRepository>();
        builder.Services.AddScoped<IGuidGenerator, GuidGenerator>();
        builder.Services.AddScoped<ICreatePaymentUseCase, CreatePaymentUseCase>();
    }
}