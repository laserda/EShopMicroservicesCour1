using Catalog.API.Data;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
var Assembly = typeof(Program).Assembly;
var connexionStr = builder.Configuration.GetConnectionString("DataBase")!;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
}); 

builder.Services.AddValidatorsFromAssembly(Assembly);

builder.Services.AddMarten(options =>
{
    options.Connection(connexionStr);
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddHealthChecks()
    .AddNpgSql(connexionStr);

var app = builder.Build();

app.MapCarter();
app.UseExceptionHandler(option => { });

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
