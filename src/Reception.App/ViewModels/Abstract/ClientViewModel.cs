using Microsoft.AspNetCore.SignalR.Client;
using Reception.App.Localization;
using Reception.App.Model;
using Reception.App.Model.PersonInfo;
using Reception.App.Network.Chat;
using Reception.Extension.Converters;
using Reception.Extension.Dictionaries;
using System.Runtime.CompilerServices;

namespace Reception.App.ViewModels.Abstract
{
    public abstract class ClientViewModel : BaseViewModel, IClientViewModel
    {
        protected readonly IClientService _clientService;


        protected ClientViewModel(MainViewModel mainViewModel, IClientService clientService)
            : base(mainViewModel)
        {
            _clientService = clientService;
            _clientService.MessageReceived += OnMessageReceived;
        }


        public void StopClientListening()
        {
            _clientService.MessageReceived -= OnMessageReceived;
        }

        protected void OnMessageReceived(int userId, Type messageType, object message)
        {
            switch (Types.Dictionary.TryGetValue(messageType))
            {
                case 1:
                    PersonReceived(message.DeserializeMessage<Person>());
                    break;
                case 2:
                    VisitorReceived(message.DeserializeMessage<Visitor>());
                    break;
                default:
                    _mainViewModel.ShowError(new ArgumentException($"Unknown message data type {messageType?.FullName ?? "null-type"}"));
                    break;
            }
        }

        protected override async Task OnViewModelInitialized()
        {
            await StartClientAsync();
        }

        protected abstract void PersonReceived(Person person);

        protected void SetNotImplementedMessage<T>(T value, [CallerMemberName] string methodName = null)
        {
            _mainViewModel.ShowError(new NotImplementedException($"{methodName} not implemented"), properties: value);
        }

        protected async Task StartClientAsync()
        {
            SetRefreshingNotification(Localizer.Instance["ConnectToChat"]);
            if (_clientService.State == HubConnectionState.Disconnected)
            {
                try
                {
                    await _clientService.StartClientAsync();
                    ClearNotification();
                }
                catch (Exception ex)
                {
                    ErrorHandler(nameof(StartClientAsync)).Invoke(ex);
                }
            }
        }

        protected abstract void VisitorReceived(Visitor visitor);
    }
}
