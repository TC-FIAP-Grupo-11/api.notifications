using FCG.Api.Notifications.Services;
using FCG.Lib.Shared.Messaging.Contracts;
using MassTransit;

namespace FCG.Api.Notifications.Consumers;

public class PaymentProcessedEventConsumer : IConsumer<PaymentProcessedEvent>
{
    private readonly ILambdaNotificationService _lambdaNotificationService;
    private readonly ILogger<PaymentProcessedEventConsumer> _logger;

    public PaymentProcessedEventConsumer(
        ILambdaNotificationService lambdaNotificationService,
        ILogger<PaymentProcessedEventConsumer> logger)
    {
        _lambdaNotificationService = lambdaNotificationService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<PaymentProcessedEvent> context)
    {
        var @event = context.Message;

        _logger.LogInformation("Received PaymentProcessedEvent for order {OrderId}", @event.OrderId);

        await _lambdaNotificationService.InvokeAsync("PaymentProcessed", @event, context.CancellationToken);
    }
}
