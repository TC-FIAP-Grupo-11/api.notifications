namespace FCG.Api.Notifications.Services;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string body, string type);
}

public class ConsoleEmailService : IEmailService
{
    private readonly ILogger<ConsoleEmailService> _logger;

    public ConsoleEmailService(ILogger<ConsoleEmailService> logger)
    {
        _logger = logger;
    }

    public Task SendEmailAsync(string email, string subject, string body, string type)
    {
        _logger.LogInformation(
            "\n========== EMAIL NOTIFICATION ==========\n" +
            "Type: {Type}\n" +
            "To: {Email}\n" +
            "Subject: {Subject}\n" +
            "Body: {Body}\n" +
            "========================================\n",
            type, email, subject, body);

        return Task.CompletedTask;
    }
}
