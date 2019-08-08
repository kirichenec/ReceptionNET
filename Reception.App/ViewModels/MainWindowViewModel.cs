using ReactiveUI;
using Reception.App.Models;
using System;

namespace Reception.App.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        public RoutingState Router { get; } = new RoutingState();

        public MainWindowViewModel()
        {
            try
            {
                if (AppSettings.IsBoss)
                {
                    Router.Navigate.Execute(new BossViewModel(this));
                }
                else
                {
                    Router.Navigate.Execute(new SubordinateViewModel(this));
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
