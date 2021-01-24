namespace Reception.App.Service.Interface
{
    public interface ISettingsService
    {
        string ChatServerPath { get; }
        string DataServerPath { get; }
        string FileServerPath { get; }
        bool IsBoss { get; }
        int PingDelay { get; }
        string UserServerPath { get; }
        string WelcomeMessage { get; }
    }
}