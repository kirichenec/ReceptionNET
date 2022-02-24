﻿using Avalonia;
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
            if (this.FindControl<T>(itemName) is { } control)
            {
                control.AttachedToVisualTree += OnAttachedToVisualTree;
            }
        }

        private void OnAttachedToVisualTree(object sender, VisualTreeAttachmentEventArgs e)
        {
            if (sender is IInputElement control)
            {
                control.AttachedToVisualTree -= OnAttachedToVisualTree;
                control.Focus();
            }
        }
    }
}