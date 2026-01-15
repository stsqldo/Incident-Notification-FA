namespace IncidentNotificationFA;

public class IncidentCreatedEvent
{
    public int IncidentId { get; set; }
    public string Title { get; set; } = "";
    public string Severity { get; set; } = "";
}