using IncidentNotification.Domain.Entities;

namespace IncidentNotification.Application;

public class SendNotification
{
    private readonly INotificationSender _emailSender;
    private readonly INotificationSender _logSender;

    public SendNotification(INotificationSender emailSender, INotificationSender logSender)
    {
        _emailSender = emailSender;
        _logSender = logSender;
    }

    public async Task ExecuteAsync(Notification notification)
    {
        if (notification == null)
            throw new ArgumentNullException(nameof(notification));

        var subject = $"New Incident #{notification.IncidentId}";
        var body = $"Incident: {notification.Title}, Severity: {notification.Severity}";

        try
        {
            // Send email notification
            await _emailSender.SendAsync(subject, body);

            // Send log / secondary notification
            await _logSender.SendAsync(subject, body);
        }
        catch (OperationCanceledException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to send incident notification.", ex);
        }
    }
}