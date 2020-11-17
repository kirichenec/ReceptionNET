using ReactiveUI;
using System;

namespace Reception.App.ViewModels
{
    public class BaseViewModel : ReactiveObject, IRoutableViewModel
    {
        public BaseViewModel()
        {
            this.ThrownExceptions.Subscribe(error => MainVM.ShowError(error));
        }

        public IScreen HostScreen { get; set; }

        public MainWindowViewModel MainVM => ViewLocator.MainVM;

        public string UrlPathSegment { get; set; }
    }
}