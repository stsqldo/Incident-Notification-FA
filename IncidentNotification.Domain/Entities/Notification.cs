namespace IncidentNotification.Domain.Entities;

public class Notification
{
    public int IncidentId { get; }
    public string Title { get; }
    public string Severity { get; }

    public Notification(int incidentId, string title, string severity)
    {
        IncidentId = incidentId;
        Title = title;
        Severity = severity;
    }
}