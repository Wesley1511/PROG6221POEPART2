namespace POEPART2
{
    internal class ChatBot
    {
        string name = "CyberSafe";

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
            Response botResponse = new Response();
            return botResponse.Respond(input);
        }
    }
}