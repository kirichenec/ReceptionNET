using Avalonia;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System.Reactive.Disposables;
using static Reception.App.Constants.ControlNames;

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

        protected void InitFirstFocusItem()
        {
            if (this.FindControl<Control>(FOCUSED_ITEM_NAME) is { } control)
            {
                control.AttachedToVisualTree += OnAttachedToVisualTree;
            }
        }

        private void OnAttachedToVisualTree(object sender, VisualTreeAttachmentEventArgs e)
        {
            if (sender is Control control)
            {
                control.AttachedToVisualTree -= OnAttachedToVisualTree;
                control.Focus();
            }
        }
    }
}