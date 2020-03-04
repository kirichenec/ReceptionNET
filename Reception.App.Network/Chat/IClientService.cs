using System;
using System.Threading.Tasks;

namespace Reception.App.Network.Chat
{
    public interface IClientService
    {
        public event Func<Exception, Task> Closed;
        public event Func<bool, Task> Connected;
        public event Func<int, Type, object, Task> MessageReceived;
        public event Func<string, Task> Reconnected;
        public event Func<Exception, Task> Reconnecting;

        Task SendAsync<T>(T value);
        Task StartClientAsync();
    }
}
