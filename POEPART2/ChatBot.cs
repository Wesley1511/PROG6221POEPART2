namespace POEPART2
{
    internal class ChatBot
    {
        string name = "CyberSafe";
        private Response botResponse = new Response(); // response object instance, initialised within the chatbot to give it "memory"

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