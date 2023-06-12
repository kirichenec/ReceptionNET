using ReactiveUI.Fody.Helpers;
using Reception.App.Localization;
using Reception.App.Service.Interface;

namespace Reception.App.ViewModels
{
    public class BossViewModel : BaseViewModel
    {
        public BossViewModel(ISettingsService settingsService, MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            SetRefreshingNotification(Localizer.Instance["BossLoadData"]);

            WelcomeMessage = settingsService.WelcomeMessage;

            ClearNotification();
        }

        [Reactive]
        public string WelcomeMessage { get; set; }
    }
}
