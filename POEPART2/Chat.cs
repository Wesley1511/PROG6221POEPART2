namespace POEPART2
{
    internal class Chat
    {
        private User ChatUser;
        private ChatBot ChatBot;

        public Chat(User user, ChatBot chatbot)
        {
            ChatUser = user;
            ChatBot = chatbot;
        }

        public void SetUserName(string name)
        {
            ChatUser.SetUserName(name);
        }

        public string GetUserName()
        {
            return ChatUser.GetUserName();
        }

        public string GetBotName()
        {
            return ChatBot.GetChatBotName();
        }

        public string GetBotResponse(string input)
        {
            return ChatBot.GetChatBotResponse(input);
        }
    }
}