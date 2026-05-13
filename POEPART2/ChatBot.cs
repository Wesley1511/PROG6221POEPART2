namespace POEPART2
{
    internal class ChatBot
    {
        string name = "CyberSafe";
        private Response botResponse = new Response(); // response object, used to generate the bots responses and to act as "memory"

        public void SetChatBotName(string name)
        {
            this.name = name;
        }

        public string GetChatBotName()
        {
            return name;
        }

        public string GetChatBotResponse(string input)
        {
            return botResponse.Respond(input);
        }

        public string GetFavouriteTopic()
        {
            return botResponse.GetFavouriteTopic();
        }
    }
}