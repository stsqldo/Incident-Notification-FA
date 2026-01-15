using IncidentNotification.Application;
using Microsoft.Extensions.Logging;

namespace IncidentNotification.Infrastructure.Logging;

public class AppInsightsLogSender : INotificationSender
{
    private readonly ILogger<AppInsightsLogSender> _logger;

    public AppInsightsLogSender(ILogger<AppInsightsLogSender> logger)
    {
        _logger = logger;
    }

    public Task SendAsync(string subject, string body)
    {
        _logger.LogInformation("{Subject} - {Body}", subject, body);
        return Task.CompletedTask;
    }
}