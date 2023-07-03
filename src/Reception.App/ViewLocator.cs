using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Reception.App.Constants;
using Reception.App.ViewModels;

namespace Reception.App
{
    public class ViewLocator : IDataTemplate
    {
        public Control Build(object param)
        {
            var name = param.GetType().FullName.Replace(AppSystem.VIEW_MODEL, AppSystem.VIEW);

            return Type.GetType(name) is Type type
                ? (Control)Activator.CreateInstance(type)
                : new TextBlock { Text = $"Not Found: {name}" };
        }

        public static bool SupportsRecycling => false;

        public bool Match(object data)
        {
            return data is BaseViewModel;
        }
    }
}
