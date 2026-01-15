using IncidentNotification.Application;
using IncidentNotification.Infrastructure.KeyVault;
using System.Net;
using System.Net.Mail;

namespace IncidentNotification.Infrastructure.Email;

public class SmtpEmailSender : INotificationSender
{
    private readonly KeyVaultSecretProvider _kv;

    public SmtpEmailSender(KeyVaultSecretProvider kv)
    {
        _kv = kv;
    }

    public async Task SendAsync(string subject, string body)
    {
        // Get configuaration values from Azure App Configuration 
        var host = await _kv.GetSecretAsync("SmtpHost");
        var user = await _kv.GetSecretAsync("SmtpUser");
        var pass = await _kv.GetSecretAsync("SmtpPassword");

        var client = new SmtpClient(host)
        {
            Credentials = new NetworkCredential(user, pass)
        };

        var mail = new MailMessage(user, user, subject, body);
        await client.SendMailAsync(mail);
    }
}