using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BrokenGrenade.Web.App.Services;

public class EmailSender : IEmailSender
{
    private readonly ILogger _logger;
    
    public AuthMessageSenderOptions Options { get; }

    public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor, ILogger<EmailSender> logger)
    {
        Options = optionsAccessor.Value;
        _logger = logger;
    }
    
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        if (string.IsNullOrEmpty(Options.SendGridKey))
            throw new InvalidOperationException("SendGrid API key is not configured.");
        
        await Execute(Options.SendGridKey, subject, htmlMessage, email);
    }

    private async Task Execute(string apiKey, string subject, string message, string toEmail)
    {
        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage
        {
            From = new EmailAddress("noreply@brokengrenade.cz", "Broken Grenade"),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        
        msg.AddTo(new EmailAddress(toEmail));
        var response = await client.SendEmailAsync(msg);
        
        if (response.IsSuccessStatusCode)
            _logger.LogInformation($"Email to {toEmail} queued successfully!");
        else
            _logger.LogError($"Failed to send email to {toEmail}!");
        
    }
}