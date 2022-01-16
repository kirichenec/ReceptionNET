using Microsoft.AspNetCore.SignalR.Client;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Enums;
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

        private readonly IMainViewModel _mainWindowViewModel;

        private readonly INetworkService<Person> _networkServiceOfPersons;

        private readonly INetworkService<FileData> _networkServiceOfFileData;

        private readonly ObservableAsPropertyHelper<IEnumerable<Person>> _searchedPersons;

        private readonly ISettingsService _settingsService;

        #endregion

        #region ctor

        public SubordinateViewModel(IMainViewModel mainWindowViewModel) : base(nameof(SubordinateViewModel), mainWindowViewModel.ShowError)
        {
            _mainWindowViewModel = mainWindowViewModel;

            SearchText = string.Empty;

            _networkServiceOfPersons ??= Locator.Current.GetService<INetworkService<Person>>();
            _networkServiceOfFileData ??= Locator.Current.GetService<INetworkService<FileData>>();
            _settingsService ??= Locator.Current.GetService<ISettingsService>();

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
                    x => x.Visitor.Comment,
                    x => x.Visitor.FirstName,
                    x => x.Visitor.Message,
                    x => x.Visitor.MiddleName,
                    x => x.Visitor.Post,
                    x => x.Visitor.SecondName,
                    selector: (person, _, __, ___, ____, _____, ______) => !person.IsNullOrEmpty());
            SendVisitorCommand = ReactiveCommand.CreateFromTask<Visitor, bool>(SendVisitor, canSendPerson);
            #endregion

            Initialized += SubordinateViewModel_Initialized;
            OnInitialized();
        }

        #endregion

        #region Properties

        public IEnumerable<Person> Persons => _searchedPersons.Value ?? Array.Empty<Person>();

        [Reactive]
        public string SearchText { get; set; }

        [Reactive]
        public Person SelectedPerson { get; set; }

        [Reactive]
        public Visitor Visitor { get; set; } = new();

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
            Visitor.CopyFrom(person);
            byte[] visitorImage = await GetVisitorPhoto(person?.PhotoId);
            Visitor.ImageSource = visitorImage;
            return true;


            async Task<byte[]> GetDefaultVisitorPhoto()
            {
                return await _settingsService.DefaultVisitorPhotoPath.GetFileBytesByPathAsync();
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
                    _mainWindowViewModel.ShowError(new ArgumentException($"Unknown message data type {messageType?.FullName ?? "null-type"}"));
                    break;
            }
        }

        private void PersonReceived(Person person)
        {
            _mainWindowViewModel.ShowError(new NotImplementedException($"{nameof(PersonReceived)} not implemented"), properties: person);
        }

        private async Task<IEnumerable<Person>> SearchPersons(string query)
        {
            if (query == null)
            {
                return Array.Empty<Person>();
            }

            var answer = await _networkServiceOfPersons.SearchAsync(query);

            switch (_mainWindowViewModel.LastErrorType)
            {
                case ErrorType.No:
                case ErrorType.Server:
                case ErrorType.System:
                case ErrorType.Request:
                case ErrorType.Connection:
                    _mainWindowViewModel.ClearErrorInfo();
                    break;
                default:
                    break;
            }
            return answer;
        }

        private async Task<bool> SendVisitor(Visitor visitor)
        {
            visitor.IncomingDate = DateTime.Now;
            await _clientService.SendAsync(visitor);
            return true;
        }

        private async Task<bool> StartClientAsync()
        {
            if (_clientService.State == HubConnectionState.Disconnected)
            {
                await _clientService.StartClientAsync();
            }
            return true;
        }

        private async Task<bool> SubordinateViewModel_Initialized()
        {
            return await StartClientAsync();
        }

        private void VisitorReceived(Visitor visitor)
        {
            _mainWindowViewModel.ShowError(new NotImplementedException($"{nameof(VisitorReceived)} not implemented"), properties: visitor);
        }

        #endregion
    }
}