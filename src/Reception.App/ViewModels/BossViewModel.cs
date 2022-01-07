using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Service.Interface;
using Splat;

namespace Reception.App.ViewModels
{
    public class BossViewModel : BaseViewModel
    {
        private readonly ISettingsService _settingsService;

        #region ctor
        public BossViewModel(IScreen screen)
        {
            _settingsService ??= Locator.Current.GetService<ISettingsService>();

            UrlPathSegment = nameof(BossViewModel);
            HostScreen = screen;

            WelcomeMessage = _settingsService.WelcomeMessage;
        }
        #endregion

        #region Properties
        [Reactive]
        public string WelcomeMessage { get; set; }
        #endregion
    }
}