using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Enums;
using Reception.App.Localization;
using Reception.App.Model.Extensions;
using Reception.App.Model.PersonInfo;
using Reception.App.Network.Chat;
using Reception.App.Network.Server;
using Reception.App.Service.Interface;
using Reception.App.ViewModels.Abstract;
using Reception.Extension;
using Reception.Extension.Converters;
using System.Reactive;
using System.Reactive.Linq;

namespace Reception.App.ViewModels
{
    public class SubordinateViewModel : ClientViewModel
    {
        private readonly IPersonNetworkService _networkServiceOfPersons;
        private readonly IFileDataNetworkService _networkServiceOfFileData;
        private readonly ISettingsService _settingsService;

        private byte[] _defaultPhotoData;
        private ObservableAsPropertyHelper<IEnumerable<Person>> _searchedPersons;


        public SubordinateViewModel(ISettingsService settingsService,
            MainViewModel mainViewModel, IClientService clientService,
            IPersonNetworkService personNetworkService, IFileDataNetworkService fileDataNetworkService)
            : base(mainViewModel, clientService)
        {
            _networkServiceOfPersons = personNetworkService;
            _networkServiceOfFileData = fileDataNetworkService;
            _settingsService = settingsService;

            InitCommands();

            OnInitialized();
        }


        public ReactiveCommand<Unit, Unit> ClearSearchPersonCommand { get; private set; }

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
        public Visitor Visitor { get; set; }


        protected override void BossDecisionReceived(BossDecision bossDecision)
        {
            // ToDo: Visualize decision + history
            SetNotImplementedMessage(bossDecision);
        }

        protected override void PersonReceived(Person person)
        {
            SetNotImplementedMessage(person);
        }

        protected override void VisitorReceived(Visitor visitor)
        {
            SetNotImplementedMessage(visitor);
        }

        private async Task ClearSearchPersonsAsync()
        {
            await SearchPersonCommand.Execute();
            SearchText = string.Empty;
        }

        private void InitClearSearchPersonCommand()
        {
            var canClearSearch = this.WhenAnyValue(
                x => x.SearchText,
                selector: query => !string.IsNullOrWhiteSpace(query) || Persons.Any());

            ClearSearchPersonCommand = ReactiveCommand
                .CreateFromTask(ClearSearchPersonsAsync, canClearSearch);

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
            Visitor = new(person)
            {
                ImageSource = await GetVisitorPhoto(person?.PhotoId, cancellationToken)
            };
            IsPhotoLoading = false;

            ClearNotification();
            return true;


            async Task<byte[]> GetDefaultVisitorPhoto(CancellationToken cancellationToken = default)
            {
                return _defaultPhotoData ??= await _settingsService.DefaultVisitorPhotoPath
                    .GetFileBytesByPathAsync(cancellationToken);
            }

            async Task<byte[]> GetVisitorPhoto(int? photoId, CancellationToken cancellationToken = default)
            {
                return photoId.HasValue
                    && (await _networkServiceOfFileData.GetByIdAsync(photoId.Value, cancellationToken)) is { } visitorImageSource
                    && !visitorImageSource.Data.IsNullOrEmpty()
                    ? visitorImageSource.Data
                    : await GetDefaultVisitorPhoto(cancellationToken);
            }
        }

        private async Task<bool> SendVisitorExecuteAsync(Visitor visitor, CancellationToken cancellationToken = default)
        {
            SetRefreshingNotification(Localizer.Instance["SubordinateSendVisitor"]);

            visitor.SetIncomingInformation();
            await _clientService.SendAsync(visitor, cancellationToken);

            ClearNotification();
            return true;
        }
    }
}
