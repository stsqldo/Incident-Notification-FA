using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace IncidentNotification.Infrastructure.KeyVault;

public class KeyVaultSecretProvider
{
    private readonly SecretClient _client;

    public KeyVaultSecretProvider(string vaultUrl)
    {
        _client = new SecretClient(new Uri(vaultUrl), new DefaultAzureCredential());
    }

    public async Task<string> GetSecretAsync(string name)
    {
        var secret = await _client.GetSecretAsync(name);
        return secret.Value.Value;
    }
}