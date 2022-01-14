using Reception.Model.Interface;

namespace Reception.App.Service.Interface
{
    public interface ISettingsService
    {
        string ChatServerPath { get; }
        string DataServerPath { get; }
        string DefaultVisitorPhotoPath { get; }
        string FileServerPath { get; }
        bool IsBoss { get; }
        int PingDelay { get; }
        IToken Token { get; set; }
        string AuthServerPath { get; }
        string WelcomeMessage { get; }
        bool WithReconnect { get; }
    }
}