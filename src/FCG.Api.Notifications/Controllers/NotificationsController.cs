using FCG.Api.Notifications.Models;
using FCG.Api.Notifications.Services;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Api.Notifications.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly IEmailService _emailService;

    public NotificationsController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request)
    {
        await _emailService.SendEmailAsync(
            request.Email,
            request.Subject,
            request.Body,
            request.Type);

        return Ok(new { message = "Email logged to console successfully" });
    }
}
