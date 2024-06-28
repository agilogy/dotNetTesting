using PaymentsApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.AddDependencies();
var app = builder.Build();
app.UseHttpsRedirection();

app.UseEndpoints();


app.Run();