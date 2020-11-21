namespace Reception.App.Network.Chat.Constants
{
    public static class ChatMethodType
    {
        public const string RECEIVER = "ReceiveMessage";
        public const string SENDER = "SendMessage";
    }

    public static class ChatMethodNames
    {
        public const string SEND_MESSAGE_BROADCAST = nameof(ChatHub<object>.SendMessageBroadcast);
    }
}