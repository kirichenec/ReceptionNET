using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Reception.App.ViewModels;

namespace Reception.App
{
    public class ViewLocator : IDataTemplate
    {
        public Control Build(object param)
        {
            var name = param.GetType().FullName.Replace("ViewModel", "View");

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
