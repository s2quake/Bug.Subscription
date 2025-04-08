using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Controllers;
using GraphQL.AspNet.Interfaces.Controllers;

internal sealed class SubscriptionController : GraphController
{
    public const string SecondChangedEventName = "SECOND_CHANGED";

    [SubscriptionRoot("onSecondChanged", typeof(SecondEventData), EventName = SecondChangedEventName)]
    public IGraphActionResult OnSecondChanged(SecondEventData eventData) => Ok(eventData);
}
