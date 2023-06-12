using Microsoft.AspNetCore.SignalR.Client;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Enums;
using Reception.App.Localization;
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
using System.Reactive;
using System.Reactive.Linq;

namespace Reception.App.ViewModels
{
    public class SubordinateViewModel : BaseViewModel
    {
        #region Fields

        private readonly IClientService _clientService;
        private readonly IPersonNetworkService _networkServiceOfPersons;
        private readonly IFileDataNetworkService _networkServiceOfFileData;
        private readonly ISettingsService _settingsService;

        private byte[] _defaultPhotoData;
        private ObservableAsPropertyHelper<IEnumerable<Person>> _searchedPersons;

        #endregion

        public SubordinateViewModel(IPersonNetworkService personNetworkService, IFileDataNetworkService fileDataNetworkService,
            ISettingsService settingsService, IClientService clientService, MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            SetRefreshingNotification(Localizer.Instance["SubordinateLoadData"]);

            SearchText = string.Empty;

            _networkServiceOfPersons = personNetworkService;
            _networkServiceOfFileData = fileDataNetworkService;
            _settingsService = settingsService;

            _clientService = clientService;
            _clientService.MessageReceived += MessageReceived;

            InitCommands();

            Initialized += OnSubordinateViewModelInitialized;
            OnInitialized();
        }

        #region Properties

        public ReactiveCommand<Unit, bool> ClearSearchPersonCommand { get; private set; }

        [Reactive]
        public bool IsPhotoLoading { get; set; }

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
            var searchEntered = this.WhenAnyValue(x => x.SearchText)
                .Throttle(TimeSpan.FromSeconds(1), RxApp.MainThreadScheduler)
                .Publish().RefCount();

            var canSearch = this.WhenAnyValue(
                x => x.SearchText,
                selector: query => !string.IsNullOrWhiteSpace(query));

            SearchPersonCommand = ReactiveCommand
                .CreateFromObservable<string, IEnumerable<Person>>(
                    execute: (searchQuery) => Observable
                        .StartAsync(ct => SearchPersonExecuteAsync(searchQuery, ct))
                        .TakeUntil(searchEntered),
                    canExecute: canSearch);

            SearchPersonCommand.ThrownExceptions.Subscribe(ErrorHandler(nameof(SearchPersonCommand)));

            _searchedPersons = SearchPersonCommand.ToProperty(this, x => x.Persons);

            var searchTrigger = searchEntered
                .Select(searchQuery => SearchPersonCommand.IsExecuting.Where(e => !e).Take(1).Select(_ => searchQuery))
                .Publish().RefCount();

            searchTrigger.Switch().InvokeCommand(SearchPersonCommand);
        }

        private void InitSelectPersonCommand()
        {
            var selectEntered = this.WhenAnyValue(x => x.SelectedPerson)
                .Publish().RefCount();

            SelectPersonCommand = ReactiveCommand
                .CreateFromObservable<Person, bool>(
                    (person) => Observable
                        .StartAsync(ct => SelectPersonExecutedAsync(person, ct))
                        .TakeUntil(selectEntered));

            SelectPersonCommand.ThrownExceptions.Subscribe(exception =>
            {
                IsPhotoLoading = false;
                ErrorHandler(nameof(SelectPersonCommand)).Invoke(exception);
            });

            var selectTrigger = selectEntered
                .Select(selectedPerson => SelectPersonCommand.IsExecuting.Where(e => !e).Take(1).Select(_ => selectedPerson))
                .Publish().RefCount();

            selectTrigger.Switch().InvokeCommand(SelectPersonCommand);
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

        private async Task<IEnumerable<Person>> SearchPersonExecuteAsync(string query, CancellationToken cancellationToken = default)
        {
            SetNotification(Localizer.Instance["SubordinateSearching"], NotificationType.Request);
            var answer = query == null
                ? Array.Empty<Person>()
                : await _networkServiceOfPersons.SearchAsync(query, cancellationToken);
            ClearNotification();
            return answer;
        }

        private async Task<bool> SelectPersonExecutedAsync(Person person, CancellationToken cancellationToken = default)
        {
            if (person == null)
            {
                return false;
            }

            SetRefreshingNotification(Localizer.Instance["SubordinateLoadPhoto"]);

            IsPhotoLoading = true;
            Visitor.CopyFrom(person);
            byte[] visitorImage = await GetVisitorPhoto(person?.PhotoId, cancellationToken);
            Visitor.ImageSource = visitorImage;
            IsPhotoLoading = false;

            ClearNotification();
            return true;


            async Task<byte[]> GetDefaultVisitorPhoto(CancellationToken cancellationToken = default)
            {
                return _defaultPhotoData = await _settingsService.DefaultVisitorPhotoPath
                    .GetFileBytesByPathAsync(cancellationToken);
            }

            async Task<byte[]> GetVisitorPhoto(int? photoId, CancellationToken cancellationToken = default)
            {
                if (!photoId.HasValue
                    || (await _networkServiceOfFileData.GetByIdAsync(photoId.Value, cancellationToken)) is not FileData visitorImageSource
                    || visitorImageSource.Data.IsNullOrEmpty())
                {
                    return await GetDefaultVisitorPhoto(cancellationToken);
                }
                return visitorImageSource.Data;
            }
        }

        private async Task<bool> SendVisitorExecuteAsync(Visitor visitor, CancellationToken cancellationToken = default)
        {
            SetRefreshingNotification(Localizer.Instance["SubordinateSendVisitor"]);

            visitor.IncomingDate = DateTime.Now;
            await _clientService.SendAsync(visitor, cancellationToken);

            ClearNotification();
            return true;
        }

        private async Task<bool> StartClientAsync()
        {
            SetRefreshingNotification(Localizer.Instance["SubordinateConnectToChat"]);
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
