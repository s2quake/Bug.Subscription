using StrawberryShake.Extensions;

internal sealed class SubscriptionTester(
    IHostApplicationLifetime appLifetime,
    ConferenceClientNS.IConferenceClient client,
    ILogger<SubscriptionTester> logger)
    : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        appLifetime.ApplicationStarted.Register(async () =>
        {
            await Task.Delay(5000, cancellationToken);
            logger.LogInformation("SubscriptionTester started.");
            client.OnSecondChanged.Watch()
                .Subscribe(item =>
                {
                    logger.LogInformation($"Second changed: {item}");
                });
        });
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
