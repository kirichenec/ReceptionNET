using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Models;

namespace Reception.App.ViewModels
{
    public class BossViewModel : BaseViewModel
    {
        #region ctor
        public BossViewModel(IScreen screen)
        {
            UrlPathSegment = nameof(BossViewModel);
            HostScreen = screen;

            WelcomeMessage = AppSettings.WelcomeMessage;
        }
        #endregion

        #region Properties
        [Reactive]
        public string WelcomeMessage { get; set; }
        #endregion
    }
}