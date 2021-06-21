using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Reception.App.Network.Auth;
using Reception.App.Network.Chat.Constants;
using Reception.App.Service.Interface;
using Reception.Model.Network;
using Splat;
using System;
using System.Threading.Tasks;

namespace Reception.App.Network.Chat
{
    public class ClientService : IClientService, IDisposable
    {
        #region Fields
        private readonly HubConnection _client;
        private readonly ISettingsService _settingsService;
        private readonly IUserService _userService;
        #endregion

        #region ctor
        public ClientService()
        {
            _settingsService ??= Locator.Current.GetService<ISettingsService>();
            _userService ??= Locator.Current.GetService<IUserService>();

            var hubBuilder =
                new HubConnectionBuilder()
                .WithUrl(Locator.Current.GetService<ISettingsService>().ChatServerPath)
                .AddNewtonsoftJsonProtocol();

            if (_settingsService.WithReconnect)
            {
                hubBuilder = hubBuilder.WithAutomaticReconnect();
            }
            _client = hubBuilder.Build();

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

        #region Methods
        private Action<int, QueryResult<object>> OnReceive =>
            (userId, message) =>
            MessageReceived?.Invoke(userId, message.DataType, message.Data);

        public async Task SendAsync<T>(T value)
        {
            var query = new QueryResult<T>(value);
            await _client.SendAsync(ChatMethodNames.SEND_MESSAGE_BROADCAST, _userService.AuthData.Id, query);
        }

        public async Task StartClientAsync()
        {
            await _client.StartAsync();
            Connected?.Invoke(true);
        }

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

        #endregion
    }
}