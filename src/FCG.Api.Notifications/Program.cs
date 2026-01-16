using FCG.Api.Notifications.Services;
using FCG.Api.Notifications.Consumers;
using FCG.Lib.Shared.Messaging.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmailService, ConsoleEmailService>();

// Configurar messaging consumers
builder.Services.AddMessagingConsumers(builder.Configuration, consumers =>
{
    consumers.AddConsumer<UserCreatedEventConsumer>();
    consumers.AddConsumer<PaymentProcessedEventConsumer>();
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FCG API v1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
