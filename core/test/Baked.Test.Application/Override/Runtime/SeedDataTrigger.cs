using Baked.Communication;

namespace Baked.Test.Override.Runtime;

public class SeedDataTrigger(IHostApplicationLifetime _lifetime, IConfiguration _configuration, ILogger<SeedDataTrigger> _logger, IClient<SeedDataTrigger> _client)
    : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        if (!_configuration.GetRequiredValue(nameof(SeedDataTrigger), defaultValue: true)) { return Task.CompletedTask; }

        _lifetime.ApplicationStarted.Register(() =>
        {
            try
            {
                _client.Send(new("seed-data", HttpMethod.Post));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) =>
        Task.CompletedTask;
}