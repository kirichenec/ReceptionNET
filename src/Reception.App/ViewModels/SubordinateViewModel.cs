using Microsoft.AspNetCore.SignalR.Client;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Model;
using Reception.App.Model.Extensions;
using Reception.App.Model.FileInfo;
using Reception.App.Model.PersonInfo;
using Reception.App.Network.Chat;
using Reception.App.Network.Server;
using Reception.App.Service.Interface;
using Reception.Extension;
using Reception.Extension.Converters;
using Reception.Extension.Dictionaries;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Reception.App.ViewModels
{
    public class SubordinateViewModel : BaseViewModel
    {
        #region Fields

        private readonly IClientService _clientService;
        private readonly INetworkService<Person> _networkServiceOfPersons;
        private readonly INetworkService<FileData> _networkServiceOfFileData;
        private readonly ISettingsService _settingsService;

        private byte[] _defaultPhotoData;
        private ObservableAsPropertyHelper<bool> _isSearching;
        private ObservableAsPropertyHelper<IEnumerable<Person>> _searchedPersons;

        #endregion

        public SubordinateViewModel(IMainViewModel mainViewModel) : base(nameof(SubordinateViewModel), mainViewModel)
        {
            SetRefreshingNotification("Loading subordinate data");

            SearchText = string.Empty;

            _networkServiceOfPersons ??= Locator.Current.GetService<INetworkService<Person>>();
            _networkServiceOfFileData ??= Locator.Current.GetService<INetworkService<FileData>>();
            _settingsService ??= Locator.Current.GetService<ISettingsService>();

            _clientService ??= Locator.Current.GetService<IClientService>();
            _clientService.MessageReceived += MessageReceived;

            InitCommands();

            Initialized += OnSubordinateViewModelInitialized;
            OnInitialized();
        }

        #region Properties

        public ReactiveCommand<Unit, bool> ClearSearchPersonCommand { get; private set; }

        [Reactive]
        public bool IsPhotoLoading { get; set; }

        // ToDo: Change to old search kick
        public bool IsSearching => _isSearching.Value;

        public IEnumerable<Person> Persons => _searchedPersons.Value ?? Array.Empty<Person>();

        public ReactiveCommand<string, IEnumerable<Person>> SearchPersonCommand { get; private set; }

        [Reactive]
        public string SearchText { get; set; }

        [Reactive]
        public Person SelectedPerson { get; set; }

        public ReactiveCommand<Person, bool> SelectPersonCommand { get; private set; }

        public ReactiveCommand<Visitor, bool> SendVisitorCommand { get; private set; }

        [Reactive]
        public Visitor Visitor { get; set; } = new();

        #endregion

        #region Methods

        private async Task<bool> ClearSearchPersonsAsync(Unit _)
        {
            await SearchPersonCommand.Execute();
            SearchText = string.Empty;
            return true;
        }

        private void InitClearSearchPersonCommand()
        {
            var canClearSearch = this.WhenAnyValue(
                x => x.SearchText,
                selector: query => !string.IsNullOrWhiteSpace(query) || Persons.Any());

            ClearSearchPersonCommand = ReactiveCommand
                .CreateFromTask<Unit, bool>(ClearSearchPersonsAsync, canClearSearch);

            ClearSearchPersonCommand.ThrownExceptions.Subscribe(ErrorHandler(nameof(ClearSearchPersonCommand)));
        }

        private void InitCommands()
        {
            InitSelectPersonCommand();
            InitSearchPersonCommand();
            InitClearSearchPersonCommand();
            InitSendPersonCommand();
        }

        private void InitSearchPersonCommand()
        {
            var canSearch = this.WhenAnyValue(
                x => x.SearchText,
                selector: query => !string.IsNullOrWhiteSpace(query));

            SearchPersonCommand = ReactiveCommand
                .CreateFromTask<string, IEnumerable<Person>>(SearchPersonExecuteAsync, canSearch);

            SearchPersonCommand.IsExecuting.ToProperty(this, x => x.IsSearching, out _isSearching);
            SearchPersonCommand.ThrownExceptions.Subscribe(ErrorHandler(nameof(SearchPersonCommand)));

            _searchedPersons = SearchPersonCommand.ToProperty(this, x => x.Persons);

            this.WhenAnyValue(x => x.SearchText)
                .Throttle(TimeSpan.FromSeconds(1), RxApp.MainThreadScheduler)
                .InvokeCommand(SearchPersonCommand);
        }

        private void InitSelectPersonCommand()
        {
            SelectPersonCommand = ReactiveCommand
                .CreateFromTask<Person, bool>(SelectPersonExecutedAsync);

            SelectPersonCommand.ThrownExceptions.Subscribe(exception =>
            {
                IsPhotoLoading = false;
                ErrorHandler(nameof(SelectPersonCommand)).Invoke(exception);
            });

            this.WhenAnyValue(x => x.SelectedPerson)
                .InvokeCommand(SelectPersonCommand);
        }

        private void InitSendPersonCommand()
        {
            var canSendPerson = this.WhenAnyValue(
                x => x.IsLoading,
                x => x.Visitor.Comment,
                x => x.Visitor.FirstName,
                x => x.Visitor.Message,
                x => x.Visitor.MiddleName,
                x => x.Visitor.Post,
                x => x.Visitor.SecondName,
                selector: (isLoading, _, __, ___, ____, _____, ______) => !Visitor.IsNullOrEmpty() && !isLoading);
            SendVisitorCommand = ReactiveCommand.CreateFromTask<Visitor, bool>(SendVisitorExecuteAsync, canSendPerson);
            SendVisitorCommand.ThrownExceptions.Subscribe(ErrorHandler(nameof(SendVisitorCommand)));
        }

        private void MessageReceived(int userId, Type messageType, object message)
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

        private void PersonReceived(Person person)
        {
            _mainViewModel.ShowError(new NotImplementedException($"{nameof(PersonReceived)} not implemented"), properties: person);
        }

        private async Task<IEnumerable<Person>> SearchPersonExecuteAsync(string query)
        {
            SetRefreshingNotification("Searching..");
            IEnumerable<Person> answer;
            if (query == null)
            {
                answer = Array.Empty<Person>();
            }
            else
            {
                answer = await _networkServiceOfPersons.SearchAsync(query);
            }
            ClearNotification();
            return answer;
        }

        private async Task<bool> SelectPersonExecutedAsync(Person person)
        {
            if (person == null)
            {
                return false;
            }

            SetRefreshingNotification("Load photo..");

            IsPhotoLoading = true;
            Visitor.CopyFrom(person);
            byte[] visitorImage = await GetVisitorPhoto(person?.PhotoId);
            Visitor.ImageSource = visitorImage;
            IsPhotoLoading = false;

            ClearNotification();
            return true;


            async Task<byte[]> GetDefaultVisitorPhoto()
            {
                return _defaultPhotoData ??= await _settingsService.DefaultVisitorPhotoPath.GetFileBytesByPathAsync();
            }

            async Task<byte[]> GetVisitorPhoto(int? photoId)
            {
                if (!photoId.HasValue
                    || (await _networkServiceOfFileData.GetById(photoId.Value)) is not FileData visitorImageSource
                    || visitorImageSource.Data.IsNullOrEmpty())
                {
                    return await GetDefaultVisitorPhoto();
                }
                return visitorImageSource.Data;
            }
        }

        private async Task<bool> SendVisitorExecuteAsync(Visitor visitor)
        {
            SetRefreshingNotification("Visitor on the way..");

            visitor.IncomingDate = DateTime.Now;
            await _clientService.SendAsync(visitor);

            ClearNotification();
            return true;
        }

        private async Task<bool> StartClientAsync()
        {
            SetRefreshingNotification("Connect to chat hub..");
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
            return true;
        }

        private async Task<bool> OnSubordinateViewModelInitialized()
        {
            return await StartClientAsync();
        }

        private void VisitorReceived(Visitor visitor)
        {
            _mainViewModel.ShowError(new NotImplementedException($"{nameof(VisitorReceived)} not implemented"), properties: visitor);
        }

        #endregion
    }
}