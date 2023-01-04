namespace BrokenGrenade.Web.App.Services;

public class AuthMessageSenderOptions
{
    public const string SendGrid = "Authentication:SendGrid";

    public string? SendGridKey { get; set; }
}

