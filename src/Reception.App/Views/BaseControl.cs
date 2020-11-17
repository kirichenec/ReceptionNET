using Avalonia.ReactiveUI;
using ReactiveUI;
using System.Reactive.Disposables;

namespace Reception.App.Views
{
    /// <summary>
    /// Workaround for model activation
    /// </summary>
    public abstract class BaseControl<TViewModel> : ReactiveUserControl<TViewModel>
        where TViewModel : class
    {
        protected BaseControl(bool activate = true)
        {
            if (activate)
            {
                this.WhenActivated(disposables =>
                {
                    Disposable.Create(() => { }).DisposeWith(disposables);
                });
            }
        }
    }
}