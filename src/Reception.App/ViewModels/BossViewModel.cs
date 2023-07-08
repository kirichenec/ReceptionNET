using ReactiveUI.Fody.Helpers;
using Reception.App.Model.PersonInfo;
using Reception.App.Network.Chat;
using Reception.App.Service.Interface;
using Reception.App.ViewModels.Abstract;

namespace Reception.App.ViewModels
{
    public class BossViewModel : ClientViewModel
    {
        public BossViewModel(ISettingsService settingsService, MainViewModel mainViewModel,
            IClientService clientService)
            : base(mainViewModel, clientService)
        {
            WelcomeMessage = settingsService.WelcomeMessage;

            ClearNotification();

            OnInitialized();
        }

        [Reactive]
        public Visitor Visitor { get; set; }

        [Reactive]
        public string WelcomeMessage { get; set; }

        protected override void PersonReceived(Person person)
        {
            SetNotImplementedMessage(person);
        }

        protected override void VisitorReceived(Visitor visitor)
        {
            Visitor = visitor;
        }
    }
}
