using Material.Colors;
using Material.Styles.Themes;
using Material.Styles.Themes.Base;

namespace Reception.App.ViewModels
{
    public static class GlobalCommand
    {
        private static PaletteHelper _paletteHelper;

        private static PaletteHelper PaletteHelper => _paletteHelper ??= new PaletteHelper();

        public static void UseMaterialUIDarkTheme()
        {
            UseMaterialUiTheme(BaseThemeMode.Dark.GetBaseTheme());
        }

        public static void UseMaterialUILightTheme()
        {
            UseMaterialUiTheme(BaseThemeMode.Light.GetBaseTheme());
        }

        private static void UseMaterialUiTheme(IBaseTheme baseTheme)
        {
            var theme = PaletteHelper.GetTheme();
            // ToDo: Get colors from settings
            //theme.SetPrimaryColor(SwatchHelper.Lookup[MaterialColor.Blue])
            //theme.SetSecondaryColor(SwatchHelper.Lookup[MaterialColor.LightBlue])
            theme.SetBaseTheme(baseTheme);
            PaletteHelper.SetTheme(theme);
        }
    }
}
