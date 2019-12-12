using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Extensions;
using Reception.App.Models;
using Reception.App.Network.Exceptions;
using Reception.App.Network.Server;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using ErrorType = Reception.App.ViewModels.MainWindowViewModel.ErrorType;

namespace Reception.App.ViewModels
{
    public class SubordinateViewModel : BaseViewModel
    {
        #region Fields
        private readonly INetworkService<Person> _networkServiceOfPersons;

        private readonly ObservableAsPropertyHelper<IEnumerable<Person>> _searchedPersons;
        #endregion

        #region ctor
        public SubordinateViewModel(IScreen screen, INetworkService<Person> networkServiceOfPersons)
        {
            UrlPathSegment = nameof(SubordinateViewModel);
            HostScreen = screen;

            _networkServiceOfPersons = networkServiceOfPersons;

            #region Init SelectPersonCommand
            SelectPersonCommand = ReactiveCommand.Create<Person, Unit?>(FillVisitorBySelected);

            this.WhenAnyValue(x => x.SelectedPerson)
                .InvokeCommand(SelectPersonCommand);
            #endregion

            #region Init SearchPersonCommand
            var canSearch = this.WhenAnyValue(x => x.SearchText, query => !string.IsNullOrWhiteSpace(query));
            SearchPersonCommand =
                ReactiveCommand.CreateFromTask<string, IEnumerable<Person>>(
                    async query =>
                    {
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
                    },
                    canSearch);
            SearchPersonCommand.ThrownExceptions.Subscribe(error => CheckError(error));

            _searchedPersons = SearchPersonCommand.ToProperty(this, x => x.Persons);

            this.WhenAnyValue(x => x.SearchText)
                .Throttle(TimeSpan.FromSeconds(1), RxApp.MainThreadScheduler)
                .InvokeCommand(SearchPersonCommand);
            #endregion
        }
        #endregion

        #region Enums
        #endregion

        #region Properties
        [Reactive]
        public Person Person { get; set; } = new Person();

        public IEnumerable<Person> Persons => _searchedPersons.Value;

        [Reactive]
        public string SearchText { get; set; }

        [Reactive]
        public Person SelectedPerson { get; set; }
        #endregion

        #region Commands
        public ReactiveCommand<Unit, IEnumerable<Person>> ClearPersonsCommand { get; set; }

        public ReactiveCommand<string, IEnumerable<Person>> SearchPersonCommand { get; set; }

        public ReactiveCommand<Person, Unit?> SelectPersonCommand { get; set; }
        #endregion

        #region Methods
        private void CheckError(Exception error)
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

        private Unit? FillVisitorBySelected(Person person)
        {
            if (!person.IsNull())
            {
                Person.CopyFrom(person);
            }
            return null;
        }
        #endregion
    }
}