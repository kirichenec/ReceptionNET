using Microsoft.AspNetCore.SignalR.Client;

namespace Reception.App.Network.Extensions
{
    internal static class HubConnectionBuilderExtensions
    {
        internal static IHubConnectionBuilder SetReconnect(this IHubConnectionBuilder builder, bool needsReconnect)
        {

            return needsReconnect
                ? builder.WithAutomaticReconnect()
                : builder;
        }
    }
}
