using System;

namespace POEPART2
{
    internal class Response
    {
        public string Respond(string input)
        {
            string response = "";

            switch (input)
            {
                case "How are you?":
                    response = "I'm doing well, thank you for asking!";
                    break;
                case "What's your purpose?":
                    response = "My purpose is to assist you with any cybersecurity related questions you may have! Feel free to ask me about anything in the cybersecurity domain, rest assured you will leave here satisfied!";
                    break;
                case "What can I ask you about?":
                    response = "You can ask me any question you can think of about cybersecurity such as, 'What are the best practices for password safety?', 'How do I protect myself from phishing attacks?' or even 'What are the current recommendations for a safe browsing experiance?'";
                    break;
                case "What are the best practices for password safety?":
                    response = "Staying safe is always important! Use strong, unique passwords for every account and avoid reusing them. Change passwords regularly and never share them with others. Enable two-factor authentication to add an extra layer of security. Need any more advice?";
                    break;
                case "How do I protect myself from phishing attacks?":
                    response = "Very wise of you to ask. Be cautious of unexpected emails, messages, or links asking for personal information. Verify the sender before clicking any links or downloading attachments. Use security software and enable multi-factor authentication for extra protection. Have any other questions?";
                    break;
                case "What are the current recommendations for a safe browsing experiance?":
                    response = "Ah, a very common worry in the modern day, here are my tips. Keep your browser and extensions up to date to protect against vulnerabilities. Avoid clicking on suspicious links or downloading files from untrusted sites. Use reputable security tools like antivirus and consider enabling ad‑blocking to reduce risky content. Any other burning questions on your mind?";
                    break;
                case "What is a DDoS attack?":
                    response = "A DDoS (Distributed Denial of Service) attack floods a website or network with excessive traffic to make it unavailable. It uses multiple compromised devices to overwhelm the target, causing slowdowns or outages. Anything else I can answer for you?";
                    break;
                case "What do I do if I have been hacked?":
                    response = "Don't panic! Immediately change all your passwords, starting with critical accounts like email and banking. Scan your devices for malware and remove any suspicious software. Report the incident to relevant authorities or service providers to secure your accounts. Hope that helps, do you have any other questions for me?";
                    break;
                case "EXIT":
                    response = "Thank you for chatting with me! If you have any more questions in the future, feel free to reach out. Stay safe online!";
                    break;
                default:
                    response = "I'm sorry, I didn't quite understand that. Can you please rephrase your question?";
                    break;
            }
            return response;
        }
    }
}