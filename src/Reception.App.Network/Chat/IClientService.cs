using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reception.App.Network.Chat
{
    public interface IClientService
    {
        public event Func<Exception, Task> Closed;
        public event Func<bool, Task> Connected;
        public event Action<int, Type, object> MessageReceived;
        public event Func<string, Task> Reconnected;
        public event Func<Exception, Task> Reconnecting;

        HubConnectionState State { get; }

        Task SendAsync<T>(T value, CancellationToken cancellationToken = default);
        Task StartClientAsync();
        Task StopClientAsync();
    }
}
