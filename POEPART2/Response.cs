using System;

namespace POEPART2
{
    internal class Response
    {
        private Random random = new Random();
        private string lastTopic = "";
        private string favouriteTopic = "";

        public void SetFavouriteTopic(string topic)
        {
            favouriteTopic = topic;
        }

        public string GetFavouriteTopic()
        {
            return favouriteTopic;
        }

        public string Respond(string input)
        {
            string lowerInput = input.ToLower();

            if (lowerInput.Contains("tell me more") || lowerInput.Contains("another tip") || lowerInput.Contains("explain more") || lowerInput.Contains("more info") || lowerInput.Contains("i am confused"))   //list of phrases that the bot can use to tell if the user wants more info or is confused
            {
                if (lastTopic != "")
                    return RespondToTopic(lastTopic);
                else
                    return "I'm not sure what topic you'd like more info on. Try asking about passwords, phishing, scams, privacy, or malware!";
            }

            if (lowerInput.Contains("i'm interested in") || lowerInput.Contains("im interested in") || lowerInput.Contains("i am interested in")) //list of phrases that will allow the bot to detect a users "favourite" phrase
            {
                string[] topics = { "password", "phishing", "scam", "privacy", "malware", "virus", "hacking" };
                foreach (string topic in topics)
                {
                    if (lowerInput.Contains(topic))
                    {
                        favouriteTopic = topic;
                        return $"Great! I'll remember that you're interested in {topic}. It's a crucial part of staying safe online. Feel free to ask me anything about it!";
                    }
                }
            }

            string detectedTopic = DetectTopic(lowerInput); //see function below
            if (detectedTopic != "")
            {
                lastTopic = detectedTopic;
                return RespondToTopic(detectedTopic);
            }

            if (lowerInput.Contains("exit")) //the keywoard for exiting the program, not really sure if this is even needed anymore...
                return "Thank you for chatting with me! If you have any more questions in the future, feel free to reach out. Stay safe online!";

            if (favouriteTopic != "")
                return $"I'm not sure I understood that. As someone interested in {favouriteTopic}, you might want to ask me more about it! Or try asking about passwords, phishing, scams, privacy, or malware.";

            return "I'm sorry, I didn't quite understand that. Try asking about passwords, phishing, scams, privacy, or malware!";
        }

        private string DetectTopic(string lowerInput)   //simply takes the input and checks if it contains any of the activating keywords the bot has responsed for
        {
            if (lowerInput.Contains("password")) return "password";
            if (lowerInput.Contains("scam")) return "scam";
            if (lowerInput.Contains("privacy")) return "privacy";
            if (lowerInput.Contains("phishing")) return "phishing";
            if (lowerInput.Contains("virus") || lowerInput.Contains("malware")) return "malware";
            if (lowerInput.Contains("hacked") || lowerInput.Contains("hack")) return "hacking";
            return "";
        }

        private string RespondToTopic(string topic) //returns a random response from a list of responses for each topic, this is to make the bot feel more natural and less repetitive
        {
            switch (topic)
            {
                case "password":
                    {
                        string[] responses =
                        {
                        "Make sure to use strong, unique passwords for each account. Avoid using personal details in your passwords!",
                        "Never reuse passwords across multiple sites. Consider using a password manager to keep track of them!",
                        "A strong password should be at least 12 characters long and include numbers, symbols, and mixed case letters."
                    };
                        return responses[random.Next(responses.Length)];
                    }
                case "scam":
                    {
                        string[] responses =
                        {
                        "Be cautious of offers that seem too good to be true — they usually are! Always verify before clicking links or sending money.",
                        "Scammers often impersonate trusted organisations. Always double-check the sender's details before responding.",
                        "If something feels off, trust your instincts. Report suspected scams to your local consumer protection authority."
                    };
                        return responses[random.Next(responses.Length)];
                    }
                case "privacy":
                    {
                        string[] responses =
                        {
                        "Review the privacy settings on your social media accounts regularly to control who can see your information.",
                        "Avoid sharing sensitive personal information online unless absolutely necessary.",
                        "Use a VPN when browsing on public Wi-Fi to help protect your privacy from prying eyes."
                    };
                        return responses[random.Next(responses.Length)];
                    }
                case "phishing":
                    {
                        string[] responses =
                        {
                        "Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organisations.",
                        "Never click suspicious links in emails or messages. When in doubt, go directly to the website yourself.",
                        "Phishing attacks often create a sense of urgency. Take a breath and verify before acting on any request."
                    };
                        return responses[random.Next(responses.Length)];
                    }
                case "malware":
                    {
                        string[] responses =
                        {
                        "Keep your antivirus software up to date and run regular scans to catch threats early.",
                        "Avoid downloading software from untrusted sources — malware is often bundled with free downloads.",
                        "If you suspect an infection, disconnect from the internet immediately and run a full system scan."
                    };
                        return responses[random.Next(responses.Length)];
                    }
                case "hacking":
                    return "Don't panic! Immediately change all your passwords, starting with critical accounts like email and banking. Scan your devices for malware and report the incident to relevant authorities.";
                default:
                    return "I'm sorry, I didn't quite understand that. Try asking about passwords, phishing, scams, privacy, or malware!";
            }
        }
    }
}