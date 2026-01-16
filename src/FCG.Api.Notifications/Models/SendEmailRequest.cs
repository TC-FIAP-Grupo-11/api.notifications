namespace FCG.Api.Notifications.Models;

public record SendEmailRequest(
    string Email,
    string Subject,
    string Body,
    string Type);
