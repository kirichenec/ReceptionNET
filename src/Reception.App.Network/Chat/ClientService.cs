using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Reception.App.Network.Extensions;
using Reception.App.Service.Interface;
using Reception.Constant;
using Reception.Model.Network;

namespace Reception.App.Network.Chat
{
    public class ClientService : IClientService, IDisposable
    {
        #region Fields
        private readonly HubConnection _client;
        private readonly ISettingsService _settingsService;
        #endregion

        #region ctor
        public ClientService(ISettingsService settingsService)
        {
            _settingsService = settingsService;

#if DEBUG
            if (_settingsService?.ChatServerPath == null)
            {
                return;
            }
#endif

            _client = new HubConnectionBuilder()
                .WithUrl(_settingsService.ChatServerPath)
                .SetReconnect(_settingsService.WithReconnect)
                .AddNewtonsoftJsonProtocol()
                .Build();

            _client.Closed += Closed;
            _client.Reconnected += Reconnected;
            _client.Reconnecting += Reconnecting;

            _client.On(ChatMethodType.RECEIVER, OnReceive);
        }
        #endregion

        #region Events
        public event Func<Exception, Task> Closed;

        public event Func<bool, Task> Connected;

        public event Action<int, Type, object> MessageReceived;

        public event Func<string, Task> Reconnected;

        public event Func<Exception, Task> Reconnecting;
        #endregion

        public HubConnectionState State => _client.State;

        #region Methods
        private Action<int, QueryResult<object>> OnReceive =>
            (userId, message) =>
            MessageReceived?.Invoke(userId, message.DataType, message.Data);

        public async Task SendAsync<T>(T value, CancellationToken cancellationToken = default)
        {
            await _client.SendAsync("SendMessageBroadcast", _settingsService.Token.UserId,
                new QueryResult<T>(value), cancellationToken);
        }

        public async Task StartClientAsync()
        {
            await _client.StartAsync();
            Connected?.Invoke(true);
        }

        public async Task StopClientAsync()
        {
            await _client.StopAsync();
            Closed?.Invoke(null);
        }

        #endregion

        #region IDisposable Support
        private bool _disposedValue = false;

        protected async virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _client.Closed -= Closed;
                    _client.Reconnected -= Reconnected;
                    _client.Reconnecting -= Reconnecting;
                    await _client.DisposeAsync();
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
