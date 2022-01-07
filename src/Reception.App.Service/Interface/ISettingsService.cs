using Reception.Model.Interface;

namespace Reception.App.Service.Interface
{
    public interface ISettingsService
    {
        string ChatServerPath { get; }
        string DataServerPath { get; }
        string DefaultVisitorPhoto { get; }
        string FileServerPath { get; }
        bool IsBoss { get; }
        int PingDelay { get; }
        IToken Token { get; set; }
        string UserServerPath { get; }
        string WelcomeMessage { get; }
        bool WithReconnect { get; }
    }
}