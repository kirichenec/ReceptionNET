using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Model;
using Reception.App.Model.Extensions;
using Reception.App.Model.FileInfo;
using Reception.App.Model.PersonInfo;
using Reception.App.Network.Chat;
using Reception.App.Network.Server;
using Reception.Extensions.Converters;
using Reception.Extensions.Dictionaries;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ErrorType = Reception.App.ViewModels.MainWindowViewModel.ErrorType;

namespace Reception.App.ViewModels
{
    public class SubordinateViewModel : BaseViewModel
    {
        #region Fields
        private readonly IClientService _clientService;

        private readonly INetworkService<Person> _networkServiceOfPersons;

        private readonly INetworkService<FileData> _networkServiceOfFileData;

        private readonly ObservableAsPropertyHelper<IEnumerable<Person>> _searchedPersons;
        #endregion

        #region ctor
        public SubordinateViewModel(IScreen screen)
        {
            UrlPathSegment = nameof(SubordinateViewModel);
            HostScreen = screen;

            SearchText = string.Empty;

            _networkServiceOfPersons ??= Locator.Current.GetService<INetworkService<Person>>();
            _networkServiceOfFileData ??= Locator.Current.GetService<INetworkService<FileData>>();

            #region Init Chat service
            _clientService ??= Locator.Current.GetService<IClientService>();
            _clientService.MessageReceived += MessageReceived;
            #endregion

            #region Init SelectPersonCommand
            SelectPersonCommand = ReactiveCommand.CreateFromTask<Person, bool>(FillVisitorBySelected);

            this.WhenAnyValue(x => x.SelectedPerson)
                .InvokeCommand(SelectPersonCommand);
            #endregion

            #region Init SearchPersonCommand
            var canSearch = this.WhenAnyValue(x => x.SearchText, query => !string.IsNullOrWhiteSpace(query));
            SearchPersonCommand =
                ReactiveCommand.CreateFromTask<string, IEnumerable<Person>>(
                    async query => await SearchPersons(query),
                    canSearch);
            SearchPersonCommand.ThrownExceptions.Subscribe(error => MainVM.ShowError(error, nameof(SearchPersons)));

            _searchedPersons = SearchPersonCommand.ToProperty(this, x => x.Persons);

            this.WhenAnyValue(x => x.SearchText)
                .Throttle(TimeSpan.FromSeconds(1), RxApp.MainThreadScheduler)
                .InvokeCommand(SearchPersonCommand);
            #endregion

            #region Init ClearSearchPersonCommand
            var canClearSearch = this.WhenAnyValue(x => x.SearchText, query => !string.IsNullOrWhiteSpace(query) || Persons.Any());
            ClearSearchPersonCommand = ReactiveCommand.CreateFromTask<Unit, bool>(ClearSearchPersons, canClearSearch);
            #endregion

            #region Init SendPersonCommand
            var canSendPerson =
                this.WhenAnyValue(
                    x => x.Visitor,
                    x => x.Visitor.Comment, x => x.Visitor.FirstName, x => x.Visitor.Message,
                    x => x.Visitor.MiddleName, x => x.Visitor.Post, x => x.Visitor.SecondName,
                    (person, _, __, ___, ____, _____, ______) =>
                    !person.IsNullOrEmpty());
            SendVisitorCommand = ReactiveCommand.CreateFromTask<Visitor, bool>(SendVisitor, canSendPerson);
            #endregion

            Initialized = SubordinateViewModel_Initialized;
            Initialized.Invoke();
        }
        #endregion

        #region Events
        private event Func<Task<bool>> Initialized;
        #endregion

        #region Properties
        public IEnumerable<Person> Persons => _searchedPersons.Value ?? Array.Empty<Person>();

        [Reactive]
        public string SearchText { get; set; }

        [Reactive]
        public Person SelectedPerson { get; set; }

        [Reactive]
        public Visitor Visitor { get; set; } = new Visitor();
        #endregion

        #region Commands
        public ReactiveCommand<Unit, bool> ClearSearchPersonCommand { get; }

        public ReactiveCommand<Unit, Unit> ChangeImageCommand { get; }

        public ReactiveCommand<string, IEnumerable<Person>> SearchPersonCommand { get; }

        public ReactiveCommand<Person, bool> SelectPersonCommand { get; }

        public ReactiveCommand<Visitor, bool> SendVisitorCommand { get; }
        #endregion

        #region Methods
        private async Task<bool> ClearSearchPersons(Unit _)
        {
            await SearchPersonCommand.Execute();
            SearchText = string.Empty;
            return true;
        }

        private async Task<bool> FillVisitorBySelected(Person person)
        {
            if (!person.IsNull())
            {
                Visitor.CopyFrom(person);
                // TODO: Change to normal getting after person-photo chain realization
                Visitor.ImageSource = (await _networkServiceOfFileData.SearchAsync("test")).FirstOrDefault()?.Value;
                return true;
            }
            return false;
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
                    MainVM.ShowError(new ArgumentException($"Unknown message data type {messageType?.FullName ?? "null-type"}"));
                    break;
            }
        }

        private void PersonReceived(Person person)
        {
            MainVM.ShowError(new NotImplementedException($"{nameof(PersonReceived)} not implemented"), properties: person);
        }

        private async Task<IEnumerable<Person>> SearchPersons(string query)
        {
            if (query == null)
            {
                return Array.Empty<Person>();
            }
            throw new Exception("test");

            var answer = await _networkServiceOfPersons.SearchAsync(query);

            if (HostScreen is MainWindowViewModel mainViewModel)
            {
                switch (mainViewModel.LastErrorType)
                {
                    case ErrorType.No:
                    case ErrorType.Server:
                    case ErrorType.System:
                    case ErrorType.Request:
                    case ErrorType.Connection:
                        mainViewModel.LastErrorType = ErrorType.No;
                        mainViewModel.ErrorMessage = string.Empty;
                        break;
                    default:
                        break;
                }
            }
            return answer;
        }

        private async Task<bool> SendVisitor(Visitor visitor)
        {
            try
            {
                visitor.IncomingDate = DateTime.Now;
                await _clientService.SendAsync(visitor);
                return true;
            }
            catch (Exception ex)
            {
                MainVM.ShowError(ex);
                return false;
            }
        }

        private async Task<bool> SubordinateViewModel_Initialized()
        {
            return await StartClientAsync();
        }

        public async Task<bool> StartClientAsync()
        {
            try
            {
                await _clientService.StartClientAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private void VisitorReceived(Visitor visitor)
        {
            MainVM.ShowError(new NotImplementedException($"{nameof(VisitorReceived)} not implemented"), properties: visitor);
        }
        #endregion
    }
}