﻿using Avalonia;
using Avalonia.Logging;
using DialogHostAvalonia;
using Material.Styles.Themes;
using Material.Styles.Themes.Base;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Reception.App.Constants;
using Reception.App.Enums;
using Reception.App.Extensions;
using Reception.App.Model.Auth;
using Reception.App.Model.PersonInfo;
using Reception.App.Network.Chat;
using Reception.App.Network.Exceptions;
using Reception.App.Network.Server;
using Reception.App.Service.Interface;
using Reception.Constant;
using Splat;
using System.Net;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection;
using static Reception.App.ViewModels.IMainViewModel;

namespace Reception.App.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IMainViewModel
    {
        #region Fields

        private readonly IClientService _clientService;
        private readonly MaterialTheme _materialThemeStyles;
        private readonly IPingService _pingService;
        private readonly ISettingsService _settingsService;

        #endregion

        public MainWindowViewModel()
        {
            _clientService = Locator.Current.GetService<IClientService>();
            _pingService = Locator.Current.GetService<IPingService>();
            _settingsService = Locator.Current.GetService<ISettingsService>();

            _materialThemeStyles = Application.Current!.LocateMaterialTheme<MaterialTheme>();

            CenterMessage = "Loading..";

            ShowError = async (error, sourceName, properties) => await ShowErrorInternal(error, sourceName, properties);

            ServerStatusMessage = ConnectionStatuses.OFFLINE.ToLower();
            StatusMessage = ConnectionStatuses.OFFLINE.ToLower();

            Router = new RoutingState();

            _ = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(_settingsService.PingDelay), RxApp.MainThreadScheduler)
                          .Subscribe(async x => await TryPing());

            ChangeThemeCommand = ReactiveCommand.Create<bool>(UseMaterialUiTheme);
            CloseSettingsCommand = ReactiveCommand.Create(CloseSettings);
            SaveSettingsCommand = ReactiveCommand.Create(SaveSettings);

            NavigateToAuthCommand = ReactiveCommand.Create(NavigateToAuth);

            AppVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            RestoreSettings();

            CenterMessage = string.Empty;
        }

        #region Properties

        [Reactive]
        public AuthenticateResponse AuthData { get; set; }

        [Reactive]
        public string CenterMessage { get; set; }

        public ReactiveCommand<bool, Unit> ChangeThemeCommand { get; }

        public ReactiveCommand<Unit, Unit> CloseSettingsCommand { get; }

        [Reactive]
        public bool IsBoss { get; set; }

        [Reactive]
        public bool IsDark { get; set; }

        [Reactive]
        public bool IsLogined { get; set; }

        public ReactiveCommand<Unit, Unit> NavigateToAuthCommand { get; }

        [Reactive]
        public string NotificationMessage { get; set; }

        [Reactive]
        public NotificationType NotificationType { get; set; }

        public RoutingState Router { get; }

        public ReactiveCommand<Unit, Unit> SaveSettingsCommand { get; }

        [Reactive]
        public string ServerStatusMessage { get; set; }

        public ShowErrorAction ShowError { get; }

        [Reactive]
        public string StatusMessage { get; set; }

        public string AppVersion { get; }

        #endregion

        #region Methods

        public void ApplyAuthData(AuthenticateResponse authData)
        {
            AuthData = authData;
            LoadIsBossMode();
        }

        public void ClearNotification()
        {
            SetNotification(null, NotificationType.No);
        }

        public void SetNotification(string message, NotificationType type)
        {
            NotificationType = type;
            NotificationMessage = message;
        }

        private static void CloseDialog()
        {
            DialogHost.Close(ControlNames.DIALOG_HOST_NAME);
        }

        private void CloseSettings()
        {
            RestoreSettings();
            CloseDialog();
        }

        private void LoadIsBossMode()
        {
            if (_settingsService.IsBoss)
            {
                Router.Navigate.Execute(new BossViewModel());
            }
            else
            {
                Router.Navigate.Execute(new SubordinateViewModel());
            }
        }

        private void NavigateToAuth()
        {
            Router.Navigate.Execute(new AuthViewModel());
        }

        private void RestoreSettings()
        {
            IsBoss = _settingsService.IsBoss;
            IsDark = _settingsService.IsDark;
            UseMaterialUiTheme(IsDark);
        }

        private void SaveSettings()
        {
            var bossModeChanged = _settingsService.IsBoss != IsBoss;
            _settingsService.IsBoss = IsBoss;
            _settingsService.IsDark = IsDark;
            CloseDialog();

            if (bossModeChanged)
            {
                NavigateToAuth();
            }
        }

        private async Task ShowErrorInternal(Exception error, string sourceName, params object[] properties)
        {
            Logger.Sink.LogException(sourceName, this, typeof(Exception), properties);

            var errorType = NotificationType.System;
            switch (error)
            {
                case NotFoundException<Person>:
                    errorType = NotificationType.Request;
                    break;
                case QueryException unauthorizedError when unauthorizedError.StatusCode == HttpStatusCode.Unauthorized:
                    await _clientService.StopClientAsync();
                    NavigateToAuth();
                    return;
                case QueryException:
                    errorType = NotificationType.Server;
                    break;
                case OperationCanceledException:
                    return;
                default:
                    break;
            }
            SetNotification($"{sourceName}: {error.Message}", errorType);
        }

        private async Task TryPing()
        {
            try
            {
                await _pingService.PingAsync();
                ServerStatusMessage = ConnectionStatuses.ONLINE.ToLower();
                if (NotificationType == NotificationType.Server)
                {
                    ClearNotification();
                }
            }
            catch (Exception ex)
            {
                ServerStatusMessage = ConnectionStatuses.OFFLINE.ToLower();
                SetNotification(ex.Message, NotificationType.Server);
            }
        }

        private void UseMaterialUiTheme(bool isDark)
        {
            _materialThemeStyles.BaseTheme = isDark ? BaseThemeMode.Dark : BaseThemeMode.Light;
        }

        #endregion
    }
}