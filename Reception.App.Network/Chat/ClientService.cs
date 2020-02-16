using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Reception.App.Network.Chat
{
    public class ClientService : IClientService, IDisposable
    {
        #region Fields
        private readonly HubConnection _client;
        #endregion

        #region ctor
        public ClientService(string serverPath, bool withReconnect = false)
        {
            var hubBuilder = new HubConnectionBuilder().WithUrl(serverPath);
            if (withReconnect)
            {
                hubBuilder = hubBuilder.WithAutomaticReconnect();
            }
            _client = hubBuilder.Build();

            _client.Closed += Closed;
            _client.Reconnected += Reconnected;
            _client.Reconnecting += Reconnecting;
        }
        #endregion

        #region Events
        public event Func<Exception, Task> Closed;

        public event Func<string, Task> Reconnected;

        public event Func<Exception, Task> Reconnecting;
        #endregion

        #region Methods
        public async Task StartAsync()
        {
            await _client.StartAsync();
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
