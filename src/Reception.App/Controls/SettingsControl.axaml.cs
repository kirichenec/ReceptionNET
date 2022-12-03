using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using System.Windows.Input;

namespace Reception.App.Controls
{
    public partial class SettingsControl : UserControl
    {
        public static readonly StyledProperty<ICommand> ChangeThemeCommandProperty =
            AvaloniaProperty.Register<SettingsControl, ICommand>(nameof(ChangeThemeCommand));

        public static readonly StyledProperty<ICommand> ChangeSystemThemeCommandProperty =
            AvaloniaProperty.Register<SettingsControl, ICommand>(nameof(ChangeSystemThemeCommand));

        public static readonly StyledProperty<ICommand> CloseSettingsCommandProperty =
            AvaloniaProperty.Register<SettingsControl, ICommand>(nameof(CloseSettingsCommand));

        public static readonly StyledProperty<bool> IsBossProperty =
            AvaloniaProperty.Register<SettingsControl, bool>(nameof(IsBoss), defaultBindingMode: BindingMode.TwoWay);

        public static readonly StyledProperty<bool> IsDarkProperty =
            AvaloniaProperty.Register<SettingsControl, bool>(nameof(IsDark), defaultBindingMode: BindingMode.TwoWay);

        public static readonly StyledProperty<bool> IsSystemThemeProperty =
            AvaloniaProperty.Register<SettingsControl, bool>(nameof(IsSystemTheme), defaultBindingMode: BindingMode.TwoWay);

        public static readonly StyledProperty<ICommand> SaveSettingsCommandProperty =
            AvaloniaProperty.Register<SettingsControl, ICommand>(nameof(SaveSettingsCommand));

        public static readonly StyledProperty<string> VersionProperty =
            AvaloniaProperty.Register<SettingsControl, string>(nameof(Version));

        public SettingsControl()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public ICommand ChangeSystemThemeCommand
        {
            get => GetValue(ChangeSystemThemeCommandProperty);
            set => SetValue(ChangeSystemThemeCommandProperty, value);
        }

        public ICommand ChangeThemeCommand
        {
            get => GetValue(ChangeThemeCommandProperty);
            set => SetValue(ChangeThemeCommandProperty, value);
        }

        public ICommand CloseSettingsCommand
        {
            get => GetValue(CloseSettingsCommandProperty);
            set => SetValue(CloseSettingsCommandProperty, value);
        }

        public bool IsBoss
        {
            get => GetValue(IsBossProperty);
            set => SetValue(IsBossProperty, value);
        }

        public bool IsDark
        {
            get => GetValue(IsDarkProperty);
            set => SetValue(IsDarkProperty, value);
        }

        public bool IsSystemTheme
        {
            get => GetValue(IsSystemThemeProperty);
            set => SetValue(IsSystemThemeProperty, value);
        }

        public ICommand SaveSettingsCommand
        {
            get => GetValue(SaveSettingsCommandProperty);
            set => SetValue(SaveSettingsCommandProperty, value);
        }

        public string Version
        {
            get => GetValue(VersionProperty);
            set => SetValue(VersionProperty, value);
        }
    }
}
