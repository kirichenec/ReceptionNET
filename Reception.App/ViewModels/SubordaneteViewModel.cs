using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Models;
using Reception.App.Network;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;

namespace Reception.App.ViewModels
{
    public class SubordinateViewModel : BaseViewModel
    {
        #region Fields
        private readonly INetworkServise<Person> _networkServiceOfPersons;

        private readonly ObservableAsPropertyHelper<IEnumerable<Person>> _searchedPersons;
        #endregion

        #region ctor
        public SubordinateViewModel(IScreen screen, INetworkServise<Person> networkServiceOfPersons)
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
                    async query => await _networkServiceOfPersons.SearchTAsync(query),
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

        public IEnumerable<Person> Persons => State == ViewModelState.Write ? _searchedPersons.Value : new List<Person>();

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
            }
        }

        private Unit? FillVisitorBySelected(Person person)
        {
            Person.CopyFrom(person);
            return null;
        }
        #endregion
    }
}