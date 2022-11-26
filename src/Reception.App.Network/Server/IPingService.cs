namespace Reception.App.Network.Server
{
    public interface IPingService
    {
        string ServerPath { get; }

        Task<string> PingAsync();
    }
}