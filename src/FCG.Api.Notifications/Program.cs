using Amazon.XRay.Recorder.Handlers.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var basePath = builder.Configuration["SWAGGER_BASE_PATH"];
    if (!string.IsNullOrEmpty(basePath))
        c.AddServer(new Microsoft.OpenApi.Models.OpenApiServer { Url = basePath });
});

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

app.MapGet("/health", () => Results.Ok(new
{
    status = "healthy",
    service = "notifications-api",
    deprecated = true,
    message = "Este serviço foi depreciado na Fase 3. O envio de notificações foi migrado para FCG.Lambda.Notification (AWS Lambda + SQS)."
}));

app.Run();
