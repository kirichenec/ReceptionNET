using Reception.App.Model.Auth;

namespace Reception.App.Service.Interface
{
    public interface ISettingsService
    {
        string ChatServerPath { get; set; }
        string DataServerPath { get; set; }
        string DefaultVisitorPhotoPath { get; set; }
        string FileServerPath { get; set; }
        bool IsBoss { get; set; }
        bool IsDark { get; set; }
        int PingDelay { get; set; }
        Token Token { get; set; }
        string AuthServerPath { get; set; }
        string WelcomeMessage { get; set; }
        bool WithReconnect { get; set; }
    }
}
