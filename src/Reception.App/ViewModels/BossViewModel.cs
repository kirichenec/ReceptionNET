using ReactiveUI.Fody.Helpers;
using Reception.App.Service.Interface;

namespace Reception.App.ViewModels
{
    public class BossViewModel : BaseViewModel
    {
        public BossViewModel(ISettingsService settingsService, MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            WelcomeMessage = settingsService.WelcomeMessage;

            ClearNotification();
        }

        [Reactive]
        public string WelcomeMessage { get; set; }
    }
}
