using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POEPART2
{
    internal class QuizGame
    {
        private List<QuizQuestion> questions;
        private List<QuizQuestion> sessionQuestions;
        private int currentIndex = -1;
        private int score = 0;
        private bool isActive = false;
        private ActivityLogger logger;

        public QuizGame(ActivityLogger sharedLogger)
        {
            questions = BuildQuestionBank();
            logger = sharedLogger;  //logger object for use in the activity tracker
        }

        public bool IsActive()
        {
            return isActive;
        }

        // Starts a new quiz session, shuffling the question order
        public string StartQuiz()
        {
            sessionQuestions = questions.OrderBy(q => Guid.NewGuid()).ToList();
            currentIndex = -1;
            score = 0;
            isActive = true;

            logger.Log("Quiz started.");

            return AdvanceToNextQuestion();
        }

        private string AdvanceToNextQuestion()      //simply moves the quiz to the next question and returns the prompt, or if there are no more questions it ends the quiz and returns the final score message
        {
            currentIndex++;

            if (currentIndex >= sessionQuestions.Count)
            {
                isActive = false;
                return BuildFinalScoreMessage();
            }

            return BuildQuestionPrompt(sessionQuestions[currentIndex]);
        }

        private string BuildQuestionPrompt(QuizQuestion q)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Question {currentIndex + 1}/{sessionQuestions.Count}: {q.QuestionText}");

            if (q.Options.Count > 0)
            {
                char label = 'A';
                foreach (var option in q.Options)
                {
                    sb.AppendLine($"{label}) {option}");
                    label++;
                }
            }
            else
            {
                sb.AppendLine("(True/False)");
            }

            return sb.ToString().TrimEnd();
        }

        // Call this with the user's answer while the quiz is active
        public string SubmitAnswer(string userAnswer)
        {
            QuizQuestion current = sessionQuestions[currentIndex];
            string normalizedAnswer = NormalizeAnswer(userAnswer, current);

            bool isCorrect = string.Equals(normalizedAnswer, current.CorrectAnswer, StringComparison.OrdinalIgnoreCase);

            StringBuilder sb = new StringBuilder();

            if (isCorrect)
            {
                score++;
                sb.AppendLine("Correct! " + current.Explanation);
            }
            else
            {
                sb.AppendLine($"Not quite. The correct answer was {current.CorrectAnswer}. {current.Explanation}");
            }

            sb.AppendLine();
            sb.Append(AdvanceToNextQuestion());

            return sb.ToString();
        }

        // Converts things like "a", "A)", "true" into the comparable format used in CorrectAnswer
        private string NormalizeAnswer(string input, QuizQuestion question)
        {
            string trimmed = input.Trim().TrimEnd(')', '.').ToUpper();

            if (question.Options.Count > 0)
            {
                // Multiple choice — accept letter (A/B/C/D)
                if (trimmed.Length >= 1 && "ABCD".Contains(trimmed[0]))
                    return trimmed[0].ToString();

                // Or accept them typing the full option text
                for (int i = 0; i < question.Options.Count; i++)
                {
                    if (string.Equals(question.Options[i], input.Trim(), StringComparison.OrdinalIgnoreCase))
                        return ((char)('A' + i)).ToString();
                }
                return trimmed;
            }
            else
            {
                // True/False
                if (trimmed.StartsWith("T")) return "TRUE";
                if (trimmed.StartsWith("F")) return "FALSE";
                return trimmed;
            }
        }

        private string BuildFinalScoreMessage()
        {
            string feedback;
            double percentage = (double)score / sessionQuestions.Count * 100;

            if (percentage >= 80)
                feedback = "Excellent work! You really know your cybersecurity basics!";
            else if (percentage >= 50)
                feedback = "Good job! You're on the right track, but there's room to learn more.";
            else
                feedback = "Keep learning! Cybersecurity awareness takes practice, don't get discouraged.";

            logger.Log($"Quiz completed with a score of {score}/{sessionQuestions.Count}.");

            return $"Quiz complete! Your final score: {score}/{sessionQuestions.Count}\n{feedback}";
        }

        private List<QuizQuestion> BuildQuestionBank()  //my list containing all my quiz questions and answers aswell as their explanations
        {
            return new List<QuizQuestion>
            {
                new QuizQuestion
                {
                    QuestionText = "What does the 'S' in HTTPS stand for?",
                    Options = new List<string> { "Secure", "Server", "System", "Standard" },
                    CorrectAnswer = "A",
                    Explanation = "HTTPS stands for HyperText Transfer Protocol Secure, meaning the connection is encrypted."
                },
                new QuizQuestion
                {
                    QuestionText = "A strong password should include a mix of letters, numbers, and symbols.",
                    Options = new List<string>(),
                    CorrectAnswer = "TRUE",
                    Explanation = "Mixing character types makes passwords much harder to crack."
                },
                new QuizQuestion
                {
                    QuestionText = "What is phishing?",
                    Options = new List<string>
                    {
                        "A type of computer virus",
                        "A fraudulent attempt to obtain sensitive information by pretending to be trustworthy",
                        "A method of encrypting data",
                        "A firewall configuration technique"
                    },
                    CorrectAnswer = "B",
                    Explanation = "Phishing tricks victims into giving up sensitive info via fake emails, messages, or websites."
                },
                new QuizQuestion
                {
                    QuestionText = "It's safe to reuse the same password across multiple important accounts.",
                    Options = new List<string>(),
                    CorrectAnswer = "FALSE",
                    Explanation = "Reusing passwords means one breach can compromise all your accounts."
                },
                new QuizQuestion
                {
                    QuestionText = "What does two-factor authentication (2FA) add to the login process?",
                    Options = new List<string>
                    {
                        "A second password",
                        "A second, independent verification step like a code or fingerprint",
                        "A longer username",
                        "Nothing, it's just a marketing term"
                    },
                    CorrectAnswer = "B",
                    Explanation = "2FA requires a second verification factor, making accounts much harder to break into even if the password is stolen."
                },
                new QuizQuestion
                {
                    QuestionText = "A VPN can help protect your privacy on public Wi-Fi.",
                    Options = new List<string>(),
                    CorrectAnswer = "TRUE",
                    Explanation = "A VPN encrypts your traffic, making it much harder for others on the same network to intercept your data."
                },
                new QuizQuestion
                {
                    QuestionText = "What is malware?",
                    Options = new List<string>
                    {
                        "A hardware malfunction",
                        "Software designed to damage, disrupt, or gain unauthorized access to a system",
                        "A type of antivirus",
                        "A network protocol"
                    },
                    CorrectAnswer = "B",
                    Explanation = "Malware is an umbrella term for malicious software like viruses, worms, ransomware, and spyware."
                },
                new QuizQuestion
                {
                    QuestionText = "You should click links in unexpected emails to verify if they're legitimate.",
                    Options = new List<string>(),
                    CorrectAnswer = "FALSE",
                    Explanation = "Clicking suspicious links can install malware or lead to phishing pages. Always verify through other means first."
                },
                new QuizQuestion
                {
                    QuestionText = "What is the main purpose of a firewall?",
                    Options = new List<string>
                    {
                        "To speed up your internet connection",
                        "To monitor and control incoming and outgoing network traffic",
                        "To store passwords securely",
                        "To back up your files"
                    },
                    CorrectAnswer = "B",
                    Explanation = "A firewall acts as a barrier between trusted and untrusted networks, blocking unauthorized access."
                },
                new QuizQuestion
                {
                    QuestionText = "Ransomware encrypts a victim's files and demands payment for the decryption key.",
                    Options = new List<string>(),
                    CorrectAnswer = "TRUE",
                    Explanation = "Ransomware is one of the most damaging forms of malware, often spread through phishing emails."
                },
                new QuizQuestion
                {
                    QuestionText = "What's a good practice when using public Wi-Fi?",
                    Options = new List<string>
                    {
                        "Access your online banking freely",
                        "Avoid sensitive transactions or use a VPN",
                        "Disable your antivirus to improve speed",
                        "Share the network password with strangers"
                    },
                    CorrectAnswer = "B",
                    Explanation = "Public Wi-Fi is often unsecured, so sensitive activity should be avoided or protected with a VPN."
                },
                new QuizQuestion
                {
                    QuestionText = "Software updates often contain important security patches.",
                    Options = new List<string>(),
                    CorrectAnswer = "TRUE",
                    Explanation = "Updates frequently fix known vulnerabilities that attackers actively try to exploit."
                }
            };
        }
    }
}