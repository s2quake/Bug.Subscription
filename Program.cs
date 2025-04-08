using System.Reflection;
using GraphQL.AspNet.Configuration;
using Microsoft.AspNetCore.WebSockets;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebSockets(options =>
{
});
builder.Services.AddGraphQL(options =>
{
    options.AddAssembly(Assembly.GetExecutingAssembly());
}).AddSubscriptions();


builder.Services.AddConferenceClient()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri("http://localhost:5255/graphql");
    })
    .ConfigureWebSocketClient(client =>
    {
        client.Uri = new Uri("ws://localhost:5255/graphql");
    });
builder.Services.AddHostedService<SecondEventPublisher>();
builder.Services.AddHostedService<SubscriptionTester>();

var app = builder.Build();

app.UseWebSockets();
app.UseGraphQL();
app.MapGet("/", () => "Hello World!");

app.Run();
