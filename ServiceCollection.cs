using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.DependencyInjection;
using AutomatedMessagesPlugin.ChatMessageType;
using AutomatedMessagesPlugin.CenterMessageType;


namespace AutomatedMessagesPlugin;
public class ServiceCollection : IPluginServiceCollection<AutomatedMessagesPlugin>
{
    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ChatMessageClass>();
        serviceCollection.AddScoped<CenterMessageClass>();
    }
}