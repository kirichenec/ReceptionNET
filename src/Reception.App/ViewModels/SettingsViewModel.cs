using Avalonia;
using DialogHostAvalonia;
using Material.Styles.Themes;
using Material.Styles.Themes.Base;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Constants;
using Reception.App.Localization;
using Reception.App.Service.Interface;
using Reception.Extension;
using System.Reactive;

namespace Reception.App.ViewModels
{
    public class SettingsViewModel
    {
        private readonly MaterialTheme _materialThemeStyles;
        private readonly Action _navigateToAuth;
        private readonly ISettingsService _settingsService;


        public SettingsViewModel(ISettingsService settingsService, Action navigateToAuth)
        {
            _settingsService = settingsService;
            _navigateToAuth = navigateToAuth;

            _materialThemeStyles = Application.Current!.LocateMaterialTheme<MaterialTheme>();

            ChangeThemeCommand = ReactiveCommand.Create<bool>(UseMaterialUiTheme);
            ChangeSystemThemeCommand = ReactiveCommand.Create<bool>(UseSystemTheme);
            CloseSettingsCommand = ReactiveCommand.Create(CloseSettings);
            SaveSettingsCommand = ReactiveCommand.Create(SaveSettings);

            Languages = Localizer.Languages.Keys.ToArray();

            RestoreSettings();
            ApplyLanguage();
        }

        public ReactiveCommand<bool, Unit> ChangeSystemThemeCommand { get; }

        public ReactiveCommand<bool, Unit> ChangeThemeCommand { get; }

        public ReactiveCommand<Unit, Unit> CloseSettingsCommand { get; }

        [Reactive]
        public bool IsBoss { get; set; }

        [Reactive]
        public bool IsDark { get; set; }

        [Reactive]
        public bool IsLogined { get; set; }

        [Reactive]
        public bool IsSystemTheme { get; set; }

        [Reactive]
        public string Language { get; set; }

        public string[] Languages { get; }

        public ReactiveCommand<Unit, Unit> SaveSettingsCommand { get; }


        private static void CloseDialog()
        {
            DialogHost.Close(ControlNames.DIALOG_HOST_NAME);
        }

        private static BaseThemeMode GetDarkThemeMode(bool isDark)
        {
            return isDark ? BaseThemeMode.Dark : BaseThemeMode.Light;
        }

        private void ApplyLanguage()
        {
            Localizer.Instance.SetLanguage(Language);
        }

        private void CloseSettings()
        {
            RestoreSettings();
            CloseDialog();
        }

        private void RestoreSettings()
        {
            IsBoss = _settingsService.IsBoss;
            IsDark = _settingsService.IsDark;
            IsSystemTheme = _settingsService.IsSystemTheme;
            Language = _settingsService.Language.IsNullOrWhiteSpace() ? Languages[0] : _settingsService.Language;
            UseSystemTheme(IsSystemTheme);
        }

        private void SaveSettings()
        {
            var bossModeChanged = _settingsService.IsBoss != IsBoss;
            _settingsService.IsBoss = IsBoss;
            _settingsService.IsDark = IsDark;
            _settingsService.IsSystemTheme = IsSystemTheme;
            _settingsService.Language = Language;

            ApplyLanguage();
            CloseDialog();

            if (bossModeChanged)
            {
                _navigateToAuth.Invoke();
            }
        }

        private void UseMaterialUiTheme(bool isDark)
        {
            _materialThemeStyles.BaseTheme = GetDarkThemeMode(isDark);
        }

        private void UseSystemTheme(bool isSystemTheme)
        {
            _materialThemeStyles.BaseTheme = isSystemTheme ? BaseThemeMode.Inherit : GetDarkThemeMode(IsDark);
        }
    }
}
