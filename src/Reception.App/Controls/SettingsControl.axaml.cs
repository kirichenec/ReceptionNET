using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using System.Windows.Input;

namespace Reception.App.Controls
{
    public partial class SettingsControl : UserControl
    {
        public static readonly StyledProperty<ICommand> ChangeThemeCommandProperty;
        public static readonly StyledProperty<ICommand> CloseSettingsCommandProperty;
        public static readonly StyledProperty<bool> IsBossProperty;
        public static readonly StyledProperty<bool> IsDarkProperty;
        public static readonly StyledProperty<ICommand> SaveSettingsCommandProperty;
        public static readonly StyledProperty<string> VersionProperty;

        public SettingsControl()
        {
            AvaloniaXamlLoader.Load(this);
        }

        static SettingsControl()
        {
            ChangeThemeCommandProperty = AvaloniaProperty.Register<SettingsControl, ICommand>(nameof(ChangeThemeCommand));
            CloseSettingsCommandProperty = AvaloniaProperty.Register<SettingsControl, ICommand>(nameof(CloseSettingsCommand));
            IsBossProperty = AvaloniaProperty.Register<SettingsControl, bool>(nameof(IsBoss), defaultBindingMode: BindingMode.TwoWay);
            IsDarkProperty = AvaloniaProperty.Register<SettingsControl, bool>(nameof(IsDark), defaultBindingMode: BindingMode.TwoWay);
            SaveSettingsCommandProperty = AvaloniaProperty.Register<SettingsControl, ICommand>(nameof(SaveSettingsCommand));
            VersionProperty = AvaloniaProperty.Register<SettingsControl, string>(nameof(Version));
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
