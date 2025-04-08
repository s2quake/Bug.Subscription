using GraphQL.AspNet.Interfaces.Subscriptions;
using GraphQL.AspNet.Schemas;

internal sealed class SecondEventPublisher(
    ISubscriptionEventRouter subscription,
    ILogger<SecondEventPublisher> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("SecondEventPublisher started.");
        var second = DateTime.Now.Second;
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(100, stoppingToken);
            }
            catch (TaskCanceledException)
            {
                break;
            }

            var newSecond = DateTime.Now.Second;
            if (newSecond != second)
            {
                var eventData = new SecondEventData
                {
                    Second = second,
                };
                RaisePublishedEvent(eventData, SubscriptionController.SecondChangedEventName);
            }

            second = newSecond;
        }
    }

    private void RaisePublishedEvent(SecondEventData eventData, string eventName)
    {
        var subscriptionEvent = new GraphQL.AspNet.SubscriptionServer.SubscriptionEvent
        {
            Id = Guid.NewGuid().ToString(),
            EventName = eventName,
            Data = eventData,
            SchemaTypeName = typeof(GraphSchema).AssemblyQualifiedName,
            DataTypeName = typeof(SecondEventData).AssemblyQualifiedName,
        };

        subscription.RaisePublishedEvent(subscriptionEvent);
    }
}
