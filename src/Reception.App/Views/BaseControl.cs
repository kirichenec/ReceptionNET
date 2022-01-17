using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
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

        protected void InitFirstFocusItem<T>(string itemName) where T : class, IControl
        {
            var hostTb = this.FindControl<T>(itemName);
            if (hostTb != null)
            {
                hostTb.AttachedToVisualTree += HostTb_AttachedToVisualTree;
            }
        }

        private void HostTb_AttachedToVisualTree(object sender, VisualTreeAttachmentEventArgs e)
        {
            var tb = (InputElement)sender;
            tb.Focus();
            tb.AttachedToVisualTree -= HostTb_AttachedToVisualTree;
        }
    }
}