using ReactiveUI.Fody.Helpers;
using Reception.App.Service.Interface;
using Splat;

namespace Reception.App.ViewModels
{
    public class BossViewModel : BaseViewModel
    {
        private readonly ISettingsService _settingsService;

        public BossViewModel(IMainViewModel mainViewModel) : base(nameof(BossViewModel), mainViewModel)
        {
            SetRefreshingNotification("Loading boss data");

            _settingsService ??= Locator.Current.GetService<ISettingsService>();

            WelcomeMessage = _settingsService.WelcomeMessage;

            ClearNotification();
        }

        [Reactive]
        public string WelcomeMessage { get; set; }
    }
}