using FCG.Api.Notifications.Services;
using FCG.Lib.Shared.Messaging.Contracts;
using MassTransit;

namespace FCG.Api.Notifications.Consumers;

public class PaymentProcessedEventConsumer : IConsumer<PaymentProcessedEvent>
{
    private readonly IEmailService _emailService;
    private readonly ILogger<PaymentProcessedEventConsumer> _logger;

    public PaymentProcessedEventConsumer(
        IEmailService emailService,
        ILogger<PaymentProcessedEventConsumer> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<PaymentProcessedEvent> context)
    {
        var evt = context.Message;
        
        _logger.LogInformation(
            "Recebido evento PaymentProcessedEvent para pedido {OrderId} - Status: {Status}",
            evt.OrderId,
            evt.Status);

        if (evt.Status == PaymentStatus.Approved)
        {
            await _emailService.SendEmailAsync(
                evt.UserEmail,
                "Compra Confirmada - FIAP Cloud Games",
                $"Olá!\n\nSua compra do jogo '{evt.GameTitle}' foi confirmada com sucesso!\n\nO jogo já está disponível em sua biblioteca.\n\nEquipe FCG",
                "PurchaseConfirmation");
        }
        else
        {
            await _emailService.SendEmailAsync(
                evt.UserEmail,
                "Falha no Pagamento - FIAP Cloud Games",
                $"Olá!\n\nInfelizmente houve um problema ao processar o pagamento do jogo '{evt.GameTitle}'.\n\n{evt.Message}\n\nPor favor, tente novamente.\n\nEquipe FCG",
                "PaymentFailed");
        }
    }
}
