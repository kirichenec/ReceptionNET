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

        private ObservableAsPropertyHelper<IEnumerable<Person>> _clearedPersons;
        private ObservableAsPropertyHelper<IEnumerable<Person>> _searchedPersons;
        #endregion

        #region ctor
        public SubordinateViewModel(IScreen screen, INetworkServise<Person> networkServiceOfPersons)
        {
            UrlPathSegment = nameof(SubordinateViewModel);
            HostScreen = screen;

            _networkServiceOfPersons = networkServiceOfPersons;
            
            #region Init SearchPersonCommand
            var canSearch = this.WhenAnyValue(x => x.SearchText, query => !string.IsNullOrWhiteSpace(query));
            SearchPersonCommand = ReactiveCommand.CreateFromTask<string, IEnumerable<Person>>(_networkServiceOfPersons.SearchTAsync, canSearch);
            SearchPersonCommand.ThrownExceptions.Subscribe(error => { CheckError(error); });

            _searchedPersons = SearchPersonCommand.ToProperty(this, x => x.Persons);

            this.WhenAnyValue(x => x.SearchText)
                .Throttle(TimeSpan.FromSeconds(1), RxApp.MainThreadScheduler)
                .InvokeCommand(SearchPersonCommand);
            #endregion
        }
        #endregion

        #region Enums
        enum PersonsSource
        {
            Clear,
            Searched
        }
        #endregion

        #region Properties

        [Reactive]
        public Person Person { get; set; }

        public IEnumerable<Person> Persons => _searchedPersons.Value;

        [Reactive]
        public string SearchText { get; set; }

        [Reactive]
        public Person SelectedPerson { get; set; }

        #endregion

        #region Commands
        public ReactiveCommand<string, IEnumerable<Person>> SearchPersonCommand { get; set; }

        public ReactiveCommand<Unit, IEnumerable<Person>> ClearPersonsCommand { get; set; }
        #endregion

        #region Methods
        private void CheckError(Exception error)
        {
            if (HostScreen is MainWindowViewModel mainViewMidel)
            {
                mainViewMidel.ErrorMessage = error.Message;
                var errorType = error.GetType();
                if (errorType == typeof(NotFoundException<Person>))
                {
                    ClearPersonsCommand.Execute();
                    return;
                }
            }
        }
        #endregion
    }
}