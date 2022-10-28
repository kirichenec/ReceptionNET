﻿using Avalonia;
using Avalonia.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
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
using System;
using System.Net;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using static Reception.App.ViewModels.IMainViewModel;

namespace Reception.App.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IMainViewModel
    {
        #region Fields

        private readonly IClientService _clientService;
        private readonly IPingService _pingService;
        private readonly ISettingsService _settingsService;

        #endregion

        public MainWindowViewModel()
        {
            _clientService ??= Locator.Current.GetService<IClientService>();
            _pingService ??= Locator.Current.GetService<IPingService>();
            _settingsService ??= Locator.Current.GetService<ISettingsService>();

            CenterMessage = "Loading..";

            ShowError = async (error, sourceName, properties) => await ShowErrorInternal(error, sourceName, properties);

            ServerStatusMessage = ConnectionStatuses.OFFLINE.ToLower();
            StatusMessage = ConnectionStatuses.OFFLINE.ToLower();

            Router = new RoutingState();

            _ = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(_settingsService.PingDelay), RxApp.MainThreadScheduler)
                          .Subscribe(async x => await TryPing());

            CenterMessage = string.Empty;

            InitChangeThemeCommand();

            NavigateToAuth();
        }

        #region Properties

        [Reactive]
        public AuthenticateResponse AuthData { get; set; }

        public ReactiveCommand<bool, Unit> ChangeThemeCommand { get; private set; }

        [Reactive]
        public string CenterMessage { get; set; }

        [Reactive]
        public bool IsLogined { get; set; }

        [Reactive]
        public string NotificationMessage { get; set; }

        [Reactive]
        public NotificationType NotificationType { get; set; }

        public RoutingState Router { get; }

        [Reactive]
        public string ServerStatusMessage { get; set; }

        public ShowErrorAction ShowError { get; }

        [Reactive]
        public string StatusMessage { get; set; }

        #endregion

        #region Methods

        public void ClearNotification()
        {
            SetNotification(null, NotificationType.No);
        }

        public void NavigateBack(AuthenticateResponse authData)
        {
            AuthData = authData;
            LoadIsBossMode();
        }

        public void SetNotification(string message, NotificationType type)
        {
            NotificationType = type;
            NotificationMessage = message;
        }

        private void InitChangeThemeCommand()
        {
            ChangeThemeCommand = ReactiveCommand.Create<bool>(SetTheme);
        }

        private void LoadIsBossMode()
        {
            if (_settingsService.IsBoss)
            {
                Router.Navigate.Execute(new BossViewModel(this));
            }
            else
            {
                Router.Navigate.Execute(new SubordinateViewModel(this));
            }
        }

        private void NavigateToAuth()
        {
            Router.Navigate.Execute(new AuthViewModel(this));
        }

        private void SetTheme(bool isDark)
        {
            if (isDark)
            {
                GlobalCommand.UseMaterialUIDarkTheme();
            }
            else
            {
                GlobalCommand.UseMaterialUILightTheme();
            }
        }

        private async Task ShowErrorInternal(Exception error, string sourceName, params object[] properties)
        {
            Logger.Sink.LogException(sourceName, this, typeof(Exception), properties);

            var errorType = NotificationType.System;
            if (error is NotFoundException<Person>)
            {
                errorType = NotificationType.Request;
            }
            else if (error is QueryException queryError)
            {
                if (queryError.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await _clientService.StopClientAsync();
                    NavigateToAuth();
                    return;
                }
                errorType = NotificationType.Server;
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

        #endregion
    }
}