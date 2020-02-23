using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Extensions;
using Reception.App.Models;
using Reception.App.Network.Chat;
using Reception.App.Network.Exceptions;
using Reception.App.Network.Server;
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
        private readonly INetworkService<Person> _networkServiceOfPersons;

        private readonly ObservableAsPropertyHelper<IEnumerable<Person>> _searchedPersons;

        private readonly IClientService _clientService;
        #endregion

        #region ctor
        public SubordinateViewModel(IScreen screen)
        {
            UrlPathSegment = nameof(SubordinateViewModel);
            HostScreen = screen;

            SearchText = string.Empty;

            _networkServiceOfPersons ??= Locator.Current.GetService<INetworkService<Person>>();
            _clientService ??= Locator.Current.GetService<IClientService>();

            #region Init SelectPersonCommand
            SelectPersonCommand = ReactiveCommand.Create<Person, bool>(FillVisitorBySelected);

            this.WhenAnyValue(x => x.SelectedPerson)
                .InvokeCommand(SelectPersonCommand);
            #endregion

            #region Init SearchPersonCommand
            var canSearch = this.WhenAnyValue(x => x.SearchText, query => !string.IsNullOrWhiteSpace(query));
            SearchPersonCommand =
                ReactiveCommand.CreateFromTask<string, IEnumerable<Person>>(
                    async query => await GetAnswer(query),
                    canSearch);
            SearchPersonCommand.ThrownExceptions.Subscribe(error => ShowError(error));

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
                    x => x.Person,
                    x => x.Person.Comment, x => x.Person.FirstName, x => x.Person.Message,
                    x => x.Person.MiddleName, x => x.Person.Post, x => x.Person.SecondName,
                    (person, _, __, ___, ____, _____, ______) =>
                    !person.IsNullOrEmpty());
            SendPersonCommand = ReactiveCommand.CreateFromTask<VisitorInfo, bool>(SendPerson, canSendPerson);
            #endregion
        }
        #endregion

        #region Properties
        [Reactive]
        public VisitorInfo Person { get; set; } = new VisitorInfo();

        public IEnumerable<Person> Persons => _searchedPersons.Value ?? Array.Empty<Person>();

        [Reactive]
        public string SearchText { get; set; }

        [Reactive]
        public Person SelectedPerson { get; set; }
        #endregion

        #region Commands
        public ReactiveCommand<Unit, bool> ClearSearchPersonCommand { get; }

        public ReactiveCommand<string, IEnumerable<Person>> SearchPersonCommand { get; }

        public ReactiveCommand<Person, bool> SelectPersonCommand { get; }

        public ReactiveCommand<VisitorInfo, bool> SendPersonCommand { get; }
        #endregion

        #region Methods
        private async Task<bool> ClearSearchPersons(Unit _)
        {
            await SearchPersonCommand.Execute();
            return true;
        }

        private bool FillVisitorBySelected(Person person)
        {
            if (!person.IsNull())
            {
                Person.CopyFrom(person);
                return true;
            }
            return false;
        }

        private async Task<IEnumerable<Person>> GetAnswer(string query)
        {
            if (query == null)
            {
                return Array.Empty<Person>();
            }

            var answer = await _networkServiceOfPersons.SearchTAsync(query);

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

        private async Task<bool> SendPerson(Person person)
        {
            await _clientService.SendAsync(person);
            return true;
        }

        private void ShowError(Exception error)
        {
            if (HostScreen is MainWindowViewModel mainViewModel)
            {
                mainViewModel.ErrorMessage = error.Message;
                if (error is NotFoundException<Person>)
                {
                    mainViewModel.LastErrorType = ErrorType.Request;
                    return;
                }
                if (error is QueryException)
                {
                    mainViewModel.LastErrorType = ErrorType.Server;
                    return;
                }
                mainViewModel.LastErrorType = ErrorType.System;
            }
        }

        #endregion
    }
}