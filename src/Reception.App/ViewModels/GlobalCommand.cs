using Avalonia;
using Material.Styles.Themes;
using Material.Styles.Themes.Base;

namespace Reception.App.ViewModels
{
    public static class GlobalCommand
    {
        private static readonly MaterialTheme _materialThemeStyles =
            Application.Current!.LocateMaterialTheme<MaterialTheme>();

        public static void UseMaterialUIDarkTheme()
        {
            UseMaterialUiTheme(BaseThemeMode.Dark);
        }

        public static void UseMaterialUILightTheme()
        {
            UseMaterialUiTheme(BaseThemeMode.Light);
        }

        private static void UseMaterialUiTheme(BaseThemeMode baseThemeMode)
        {
            _materialThemeStyles.BaseTheme = baseThemeMode;
        }
    }
}
