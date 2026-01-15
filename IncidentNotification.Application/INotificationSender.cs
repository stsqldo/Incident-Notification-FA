namespace IncidentNotification.Application;

public interface INotificationSender
{
    Task SendAsync(string subject, string body);
}