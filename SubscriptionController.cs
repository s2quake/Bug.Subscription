using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Controllers;

internal sealed class QueryController : GraphController
{
    [QueryRoot("Second")]
    public int Second() => DateTime.Now.Second;
}