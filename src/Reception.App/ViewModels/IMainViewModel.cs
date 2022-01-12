using ReactiveUI;
using Reception.App.Enums;
using Reception.App.Model.Auth;
using System;
using System.Runtime.CompilerServices;

namespace Reception.App.ViewModels
{
    public interface IMainViewModel : IScreen
    {
        delegate void ShowErrorAction(Exception error, [CallerMemberName] string sourceName = null, params object[] properties);

        AuthenticateResponse AuthData { get; set; }

        ErrorType LastErrorType { get; set; }

        ShowErrorAction ShowError { get; }

        void ClearErrorInfo();

        void NavigateBack(AuthenticateResponse authData);
    }
}