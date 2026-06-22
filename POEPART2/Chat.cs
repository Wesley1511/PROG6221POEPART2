namespace POEPART2
{
    internal class Chat
    {
        private User ChatUser;
        private ChatBot ChatBot;
        private TaskAssistant taskAssistant = new TaskAssistant();
        private QuizGame quizGame = new QuizGame();

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

        public string GetFavouriteTopic()
        {
            return ChatBot.GetFavouriteTopic();
        }

        public string GetBotResponse(string input)
        {
            string lowerInput = input.ToLower().Trim();

            // If a quiz is in progress, treat all input as quiz answers
            if (quizGame.IsActive())
            {
                return quizGame.SubmitAnswer(input);
            }

            // Start a new quiz
            if (lowerInput.Contains("quiz") || lowerInput.Contains("play a game") || lowerInput.Contains("start game"))
            {
                return quizGame.StartQuiz();
            }

            // If we're waiting on a reminder reply, handle that first
            if (taskAssistant.IsAwaitingReminderReply())
            {
                return taskAssistant.HandleReminderReply(input);
            }

            // Check if this input is a task command
            if (taskAssistant.TryHandleTaskCommand(input, out string taskResponse))
            {
                return taskResponse;
            }

            // Otherwise, fall back to the normal cybersecurity chatbot
            return ChatBot.GetChatBotResponse(input);
        }
    }
}