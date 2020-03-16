﻿using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Model;
using Reception.App.Model.Extensions;
using Reception.App.Model.PersonInfo;
using Reception.App.Network.Chat;
using Reception.App.Network.Exceptions;
using Reception.App.Network.Server;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ErrorType = Reception.App.ViewModels.MainWindowViewModel.ErrorType;

namespace Reception.App.ViewModels
{
    public class SubordinateViewModel : BaseViewModel
    {
        #region Fields
        private readonly IClientService _clientService;

        private readonly INetworkService<Person> _networkServiceOfPersons;

        private readonly ObservableAsPropertyHelper<IEnumerable<Person>> _searchedPersons;
        #endregion

        #region ctor
        public SubordinateViewModel(IScreen screen)
        {
            UrlPathSegment = nameof(SubordinateViewModel);
            HostScreen = screen;

            SearchText = string.Empty;

            _networkServiceOfPersons ??= Locator.Current.GetService<INetworkService<Person>>();

            #region Init Chat service
            _clientService ??= Locator.Current.GetService<IClientService>();
            _clientService.MessageReceived += MessageReceived;
            #endregion

            #region Init SelectPersonCommand
            SelectPersonCommand = ReactiveCommand.Create<Person, bool>(FillVisitorBySelected);

            this.WhenAnyValue(x => x.SelectedPerson)
                .InvokeCommand(SelectPersonCommand);
            #endregion

            #region Init SearchPersonCommand
            var canSearch = this.WhenAnyValue(x => x.SearchText, query => !string.IsNullOrWhiteSpace(query));
            SearchPersonCommand =
                ReactiveCommand.CreateFromTask<string, IEnumerable<Person>>(
                    async query => await SearchPersons(query),
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
        [Reactive]
        public Visitor Visitor { get; set; } = new Visitor();

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

        public ReactiveCommand<Visitor, bool> SendVisitorCommand { get; }
        #endregion

        #region Methods
        private async Task<bool> ClearSearchPersons(Unit _)
        {
            await SearchPersonCommand.Execute();
            SearchText = string.Empty;
            return true;
        }

        private static T DeserializeMessage<T>(object message)
        {
            return JsonSerializer.Deserialize<T>(message.ToString());
        }

        private bool FillVisitorBySelected(Person person)
        {
            if (!person.IsNull())
            {
                Visitor.CopyFrom(person);
                return true;
            }
            return false;
        }

        private Task MessageReceived(int userId, Type messageType, Visitor message)
        {
            int typeId = 0;
            if (messageType != null)
            {
                Types.Dictionary.TryGetValue(messageType, out typeId);
            }
            switch (typeId)
            {
                case 1:
                    PersonReceived(DeserializeMessage<Person>(message));
                    break;
                case 2:
                    VisitorReceived(DeserializeMessage<Visitor>(message));
                    break;
                default:
                    ShowError(new ArgumentException($"Unknown message data type {messageType?.FullName ?? "null-type"}"));
                    break;
            }

            return Task.FromResult(true);
        }

        private void PersonReceived(Person person)
        {
            throw new NotImplementedException($"{nameof(PersonReceived)}({person})");
        }

        private async Task<IEnumerable<Person>> SearchPersons(string query)
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
                ShowError(ex);
                return false;
            }
        }

        private async Task<bool> SubordinateViewModel_Initialized()
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

        private void VisitorReceived(Visitor visitor)
        {
            throw new NotImplementedException($"{nameof(VisitorReceived)}({visitor})");
        }
        #endregion
    }
}