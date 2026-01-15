using IncidentNotification.Application;
using IncidentNotification.Infrastructure.Email;
using IncidentNotification.Infrastructure.Logging;
using IncidentNotification.Infrastructure.KeyVault;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        var vaultUrl = Environment.GetEnvironmentVariable("KeyVaultUrl");

        services.AddSingleton(new KeyVaultSecretProvider(vaultUrl));

        services.AddScoped<INotificationSender, SmtpEmailSender>();
        services.AddScoped<INotificationSender, AppInsightsLogSender>();

        services.AddScoped<SendNotification>();
    })
    .Build();

host.Run();