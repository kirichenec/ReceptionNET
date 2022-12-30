using Microsoft.AspNetCore.SignalR;
using Reception.Constant;

namespace Reception.Server.Chat
{
    public class ChatHub<TData> : Hub where TData : class, new()
    {
        public async Task SendMessageBroadcast(int userId, TData messageData)
        {
            await Clients.All.SendAsync(ChatMethodType.RECEIVER, userId, messageData);
        }
    }
}
