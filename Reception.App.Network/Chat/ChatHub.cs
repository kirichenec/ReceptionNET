using Microsoft.AspNetCore.SignalR;
using Reception.App.Network.Chat.Constants;
using System.Threading.Tasks;

namespace Reception.App.Network.Chat
{
    public class ChatHub<TData> : Hub where TData : class, new()
    {
        public async Task SendMessageBroadcast(int userId, TData messageData)
        {
            await Clients.All.SendAsync(ChatMethodType.RECEIVER, userId, messageData);
        }
    }
}