namespace POEPART2
{
    internal class Chat
    {
        private User ChatUser;
        private ChatBot ChatBot;
        private ActivityLogger activityLogger = new ActivityLogger();
        private TaskAssistant taskAssistant;
        private QuizGame quizGame;

        public Chat(User user, ChatBot chatbot)
        {
            ChatUser = user;
            ChatBot = chatbot;
            taskAssistant = new TaskAssistant(activityLogger);
            quizGame = new QuizGame(activityLogger);
        }

        public void SetUserName(string name)
        {
            ChatUser.SetUserName(name);
            activityLogger.Log($"User set their name to \"{name}\".");
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

            // View the activity log
            if (lowerInput.Contains("activity log") || lowerInput.Contains("view log") || lowerInput.Contains("show log"))
            {
                return activityLogger.GetLogAsText();
            }

            if (quizGame.IsActive())
            {
                return quizGame.SubmitAnswer(input);
            }

            if (lowerInput.Contains("quiz") || lowerInput.Contains("play a game") || lowerInput.Contains("start game"))
            {
                return quizGame.StartQuiz();
            }

            if (taskAssistant.IsAwaitingReminderReply())
            {
                return taskAssistant.HandleReminderReply(input);
            }

            if (taskAssistant.TryHandleTaskCommand(input, out string taskResponse))
            {
                return taskResponse;
            }

            activityLogger.Log($"User asked: \"{input}\"");
            return ChatBot.GetChatBotResponse(input);
        }
    }
}