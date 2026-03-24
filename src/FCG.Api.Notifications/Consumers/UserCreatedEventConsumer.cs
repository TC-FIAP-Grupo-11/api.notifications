using FCG.Api.Notifications.Services;
using FCG.Lib.Shared.Messaging.Contracts;
using MassTransit;

namespace FCG.Api.Notifications.Consumers;

public class UserCreatedEventConsumer : IConsumer<UserCreatedEvent>
{
    private readonly ILambdaNotificationService _lambdaNotificationService;
    private readonly ILogger<UserCreatedEventConsumer> _logger;

    public UserCreatedEventConsumer(
        ILambdaNotificationService lambdaNotificationService,
        ILogger<UserCreatedEventConsumer> logger)
    {
        _lambdaNotificationService = lambdaNotificationService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        var @event = context.Message;

        _logger.LogInformation("Received UserCreatedEvent for user {UserId}", @event.UserId);

        await _lambdaNotificationService.InvokeAsync("UserCreated", @event, context.CancellationToken);
    }
}
