using FCG.Api.Notifications.Services;
using FCG.Lib.Shared.Messaging.Contracts;
using MassTransit;

namespace FCG.Api.Notifications.Consumers;

public class UserCreatedEventConsumer : IConsumer<UserCreatedEvent>
{
    private readonly IEmailService _emailService;
    private readonly ILogger<UserCreatedEventConsumer> _logger;

    public UserCreatedEventConsumer(
        IEmailService emailService,
        ILogger<UserCreatedEventConsumer> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        var evt = context.Message;
        
        _logger.LogInformation(
            "Recebido evento UserCreatedEvent para usuário {UserId} - {Email}",
            evt.UserId,
            evt.Email);

        await _emailService.SendEmailAsync(
            evt.Email,
            "Bem-vindo à FIAP Cloud Games!",
            $"Olá {evt.Name},\n\nSeja bem-vindo à FIAP Cloud Games! Estamos felizes em tê-lo conosco.\n\nEquipe FCG",
            "Welcome");
    }
}
