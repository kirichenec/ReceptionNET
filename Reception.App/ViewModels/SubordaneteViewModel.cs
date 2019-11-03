using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Models;
using Reception.App.Network;
using Reception.App.Network.Exceptions;
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

        private ObservableAsPropertyHelper<IEnumerable<Person>> _searchedPersons;
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
            SearchPersonCommand =
                ReactiveCommand.CreateFromTask<string, IEnumerable<Person>>(
                    async query =>
                    {
                        if (string.IsNullOrWhiteSpace(query))
                        {
                            return new List<Person>();
                        }
                        return await _networkServiceOfPersons.SearchTAsync(query);
                    });
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
                var errorType = error.GetType();
                if (errorType == typeof(NotFoundException<Person>))
                {
                    SearchPersonCommand.Execute();
                }
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