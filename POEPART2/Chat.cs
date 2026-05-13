namespace POEPART2
{
    internal class Chat // class that represents a chat between a user and a chatbot, basically an intermediary for the users and chatbot
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

        public string GetFavouriteTopic()
        {
            return ChatBot.GetFavouriteTopic();
        }
    }
}