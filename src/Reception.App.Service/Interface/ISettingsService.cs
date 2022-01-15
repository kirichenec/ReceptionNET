using Reception.App.Model.Auth;

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
        Token Token { get; set; }
        string AuthServerPath { get; }
        string WelcomeMessage { get; }
        bool WithReconnect { get; }
    }
}