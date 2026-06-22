using System;
using System.Text;
using System.Text.RegularExpressions;

namespace POEPART2
{
    internal class TaskAssistant
    {
        private TaskRepository repository = new TaskRepository();
        private int? pendingTaskId = null; // tracks a task waiting on a reminder reply
        private ActivityLogger logger;

        public TaskAssistant(ActivityLogger sharedLogger)
        {
            logger = sharedLogger;
        }

        // Predefined descriptions for common cybersecurity tasks
        private string GetDefaultDescription(string title)
        {
            string lower = title.ToLower();

            if (lower.Contains("two-factor") || lower.Contains("2fa"))
                return "Enable two-factor authentication on your important accounts for an extra layer of security.";
            if (lower.Contains("privacy"))
                return "Review account privacy settings to ensure your data is protected.";
            if (lower.Contains("password"))
                return "Update your passwords to strong, unique values for each account.";
            if (lower.Contains("backup"))
                return "Back up your important files to a secure location.";
            if (lower.Contains("update") || lower.Contains("software"))
                return "Install the latest software updates to patch known security vulnerabilities.";

            return $"Cybersecurity task: {title}";
        }

        public bool IsAwaitingReminderReply()
        {
            return pendingTaskId != null;
        }

        // Call this when a task is awaiting a reminder reply, to handle "yes"/"no"/"in 3 days" etc
        public string HandleReminderReply(string input)
        {
            string lower = input.ToLower().Trim();

            if (lower.StartsWith("no"))
            {
                pendingTaskId = null;
                logger.Log("User declined to set a reminder for a task.");
                return "No problem, no reminder has been set for that task.";
            }

            Match match = Regex.Match(lower, @"(\d+)\s*day");
            if (match.Success)
            {
                int days = int.Parse(match.Groups[1].Value);
                DateTime reminderDate = DateTime.Now.AddDays(days);

                repository.SetReminder(pendingTaskId.Value, reminderDate);
                logger.Log($"Reminder set for task #{pendingTaskId.Value} in {days} day(s) ({reminderDate:dd MMM yyyy}).");
                pendingTaskId = null;

                return $"Got it! I'll remind you in {days} day{(days == 1 ? "" : "s")} (on {reminderDate:dd MMM yyyy}).";
            }

            if (lower.StartsWith("yes"))
            {
                return "Sure! How many days from now should I remind you?";
            }

            return "I didn't quite catch that. Please reply with something like 'yes, remind me in 3 days' or 'no'.";
        }

        // Returns true if this input was handled as a task command
        public bool TryHandleTaskCommand(string input, out string response)
        {
            string lower = input.ToLower().Trim();

            Match addMatch = Regex.Match(input, @"add task\s*-?\s*(.+)", RegexOptions.IgnoreCase);
            if (addMatch.Success)
            {
                string title = addMatch.Groups[1].Value.Trim();
                string description = GetDefaultDescription(title);

                int newId = repository.AddTask(title, description, null);
                pendingTaskId = newId;

                logger.Log($"Task added: \"{title}\" (ID #{newId}).");

                response = $"Task added with description \"{description}\" Would you like a reminder?";
                return true;
            }

            if (lower.Contains("view tasks") || lower.Contains("show tasks") || lower.Contains("list tasks") || lower.Contains("my tasks"))
            {
                response = BuildTaskListResponse();
                return true;
            }

            Match completeMatch = Regex.Match(lower, @"(complete|finish|done with)\s*task\s*(\d+)");
            if (completeMatch.Success)
            {
                int id = int.Parse(completeMatch.Groups[2].Value);
                bool success = repository.MarkTaskCompleted(id);

                if (success)
                    logger.Log($"Task #{id} marked as completed.");

                response = success
                    ? $"Task {id} marked as completed. Great job staying on top of your cybersecurity!"
                    : $"I couldn't find a task with ID {id}.";
                return true;
            }

            Match deleteMatch = Regex.Match(lower, @"(delete|remove)\s*task\s*(\d+)");
            if (deleteMatch.Success)
            {
                int id = int.Parse(deleteMatch.Groups[2].Value);
                bool success = repository.DeleteTask(id);

                if (success)
                    logger.Log($"Task #{id} deleted.");

                response = success
                    ? $"Task {id} has been deleted."
                    : $"I couldn't find a task with ID {id}.";
                return true;
            }

            response = null;
            return false;
        }

        private string BuildTaskListResponse()
        {
            var tasks = repository.GetAllTasks();

            if (tasks.Count == 0)
                return "You don't have any tasks yet. Try saying 'Add task - enable two-factor authentication' to create one!";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Here are your tasks:");

            foreach (var task in tasks)
            {
                string status = task.IsCompleted ? "[Completed]" : "[Pending]";
                string reminder = task.ReminderDate.HasValue ? $" | Reminder: {task.ReminderDate:dd MMM yyyy}" : "";
                sb.AppendLine($"#{task.Id} {status} {task.Title}{reminder}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}