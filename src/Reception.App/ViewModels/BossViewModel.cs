using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Localization;
using Reception.App.Model.PersonInfo;
using Reception.App.Network.Chat;
using Reception.App.Service.Interface;
using Reception.App.ViewModels.Abstract;
using System.Reactive;

namespace Reception.App.ViewModels
{
    public class BossViewModel : ClientViewModel
    {
        public BossViewModel(ISettingsService settingsService,
            MainViewModel mainViewModel, IClientService clientService)
            : base(mainViewModel, clientService)
        {
            WelcomeMessage = settingsService.WelcomeMessage;

            InitCommands();

            OnInitialized();
        }


        public ReactiveCommand<Visitor, Unit> AllowCommand { get; private set; }

        public ReactiveCommand<Visitor, Unit> DenyCommand { get; private set; }

        [Reactive]
        public Visitor Visitor { get; set; }

        public ReactiveCommand<Visitor, Unit> WaitCommand { get; private set; }

        [Reactive]
        public string WelcomeMessage { get; set; }

        protected override void BossDecisionReceived(BossDecision bossDecision)
        {
            SetNotImplementedMessage(bossDecision);
        }

        protected override void PersonReceived(Person person)
        {
            SetNotImplementedMessage(person);
        }

        protected override void VisitorReceived(Visitor visitor)
        {
            Visitor = visitor;
        }

        private void InitCommands()
        {
            var canSendDecision = this.WhenAnyValue(
                x => x.IsLoading,
                selector: (isLoading) => !isLoading);

            AllowCommand = InitDecisionCommand(Decision.Allow, nameof(AllowCommand));
            DenyCommand = InitDecisionCommand(Decision.Deny, nameof(DenyCommand));
            WaitCommand = InitDecisionCommand(Decision.Wait, nameof(WaitCommand));


            ReactiveCommand<Visitor, Unit> InitDecisionCommand(Decision allow, string commandName)
            {
                var command = ReactiveCommand.CreateFromTask<Visitor>(
                    visitor => SendDecisionAsync(visitor, allow),
                    canSendDecision);
                command.ThrownExceptions.Subscribe(ErrorHandler(commandName));
                return command;
            }
        }

        private async Task SendDecisionAsync(Visitor visitor, Decision decision, CancellationToken cancellationToken = default)
        {
            SetRefreshingNotification(Localizer.Instance["BossSendDecision"]);
            var bossDecision = new BossDecision { Guid = visitor.Guid, Value = decision };
            await _clientService.SendAsync(bossDecision, cancellationToken);
            ClearNotification();
        }
    }
}
