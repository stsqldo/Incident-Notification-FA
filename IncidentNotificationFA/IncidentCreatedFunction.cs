using System.Text.Json;
using IncidentNotification.Application;
using IncidentNotification.Domain.Entities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace IncidentNotificationFA;

public class IncidentCreatedFunction
{
    private readonly SendNotification _sendIncidentNotification;

    public IncidentCreatedFunction(SendNotification useCase)
    {
        _sendIncidentNotification = useCase;
    }

    [Function("SendNotification")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        var payload = await JsonSerializer.DeserializeAsync<IncidentCreatedEvent>(req.Body);

        var notification = new Notification(
            payload!.IncidentId,
            payload.Title,
            payload.Severity);

        await _sendIncidentNotification.ExecuteAsync(notification);

        return req.CreateResponse(System.Net.HttpStatusCode.OK);
    }
}