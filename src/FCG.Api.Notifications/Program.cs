using Amazon;
using Amazon.Lambda;
using Amazon.Runtime;
using FCG.Api.Notifications.Consumers;
using FCG.Api.Notifications.Services;
using FCG.Lib.Shared.Messaging.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var basePath = builder.Configuration["SWAGGER_BASE_PATH"];
    if (!string.IsNullOrEmpty(basePath))
        c.AddServer(new Microsoft.OpenApi.Models.OpenApiServer { Url = basePath });
});

// AWS Lambda client
builder.Services.AddSingleton<IAmazonLambda>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var accessKey = config["AWS:AccessKeyId"];
    var secretKey = config["AWS:SecretAccessKey"];
    var sessionToken = config["AWS:SessionToken"];
    var region = config["AWS:Region"] ?? "us-east-1";

    AWSCredentials credentials;
    if (!string.IsNullOrEmpty(sessionToken))
        credentials = new SessionAWSCredentials(accessKey, secretKey, sessionToken);
    else if (!string.IsNullOrEmpty(accessKey) && !string.IsNullOrEmpty(secretKey))
        credentials = new BasicAWSCredentials(accessKey, secretKey);
    else
        credentials = FallbackCredentialsFactory.GetCredentials();

    return new AmazonLambdaClient(credentials, RegionEndpoint.GetBySystemName(region));
});

builder.Services.AddScoped<ILambdaNotificationService, LambdaNotificationService>();

// Messaging - Consumers
builder.Services.AddMessagingConsumers(builder.Configuration, consumers =>
{
    consumers.AddConsumer<UserCreatedEventConsumer>();
    consumers.AddConsumer<PaymentProcessedEventConsumer>();
}, "notifications");

var app = builder.Build();

app.UseXRay("fcg-notifications-api");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "FCG API v1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

app.MapControllers();

app.MapGet("/health", () => Results.Ok(new { status = "healthy", service = "notifications-api" }));

app.Run();
