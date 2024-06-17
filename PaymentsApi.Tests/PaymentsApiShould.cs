using System.Net.Http.Headers;
using dotNetTesting.Payments;
using dotNetTesting.Payments.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PaymentsApi.Tests;

public class FakeCreatePaymentUseCase : ICreatePaymentUseCase
{
    public Task<PaymentId> Invoke(CreatePaymentRequest request)
    {
        return new Task<PaymentId>(() => new PaymentId("1234"));
    }
}

public class PaymentsApiShould
{
    private HttpClient _testClient; 
    public PaymentsApiShould()
    {
        var builder = WebApplication.CreateBuilder();
        builder.AddDependencies();
        builder.Services.Replace(new ServiceDescriptor(typeof(ICreatePaymentUseCase), new FakeCreatePaymentUseCase()));
        builder.WebHost.UseTestServer();
        var app = builder.Build();
        app.Start();
        _testClient = app.GetTestClient();
    }

    [Fact]
    public async void Test1()
    {
        var request = new CreatePaymentRequest(
                new MonetaryAmount(10, Currency.Eur), new EntityId("Halcon"), new EntityId("Hotel")
            );
        var jsonRequest = JsonSerializer.Serialize(request);
        var content = new StringContent(jsonRequest);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var response = await _testClient.PostAsync("/payments", content);
        var responseContent = await response.Content.ReadAsStringAsync();
        var paymentId = JsonConvert.DeserializeObject<PaymentId>(responseContent);
        
        
        //response.EnsureSuccessStatusCode();
        Assert.Equal(new PaymentId("1234"), paymentId);
        
    }
}