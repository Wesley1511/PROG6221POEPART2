using System.Media;
using System.Windows;
using System.Windows.Input;
using System.Media;

namespace POEPART2
{
    public partial class MainWindow : Window
    {
        private Chat chat;
        private bool chatStarted = false;

        public MainWindow()
        {
            InitializeComponent();

            User user = new User();
            ChatBot bot = new ChatBot();
            chat = new Chat(user, bot);

            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Audio\Greeting.wav");
            SoundPlayer player = new SoundPlayer(path);
            player.Play();


            chatLog.AppendText("[CyberSafe] Hello! My name is CyberSafe and I will be your cybersecurity assistant today! Please enter your name above to get started!\n");
        }

        private void StartChat_Click(object sender, RoutedEventArgs e)
        {
            BeginChat();
        }

        private void nameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                BeginChat();
        }

        private void BeginChat()
        {
            if (chatStarted || string.IsNullOrWhiteSpace(nameBox.Text)) return;

            chat.SetUserName(nameBox.Text);
            chatStarted = true;

            namePanel.Visibility = Visibility.Collapsed;
            inputBox.IsEnabled = true;
            sendButton.IsEnabled = true;

            chatLog.AppendText($"[CyberSafe] Nice to meet you {chat.GetUserName()}! How can I assist you today? Type EXIT to end the chat.\n");
            chatLog.ScrollToEnd();
            inputBox.Focus();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void inputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SendMessage();
        }

        private void SendMessage()
        {
            if (!chatStarted || string.IsNullOrWhiteSpace(inputBox.Text)) return;

            string userInput = inputBox.Text.Trim();
            string botResponse = chat.GetBotResponse(userInput);

            chatLog.AppendText($"[{chat.GetUserName()}] {userInput}\n");
            chatLog.AppendText($"[{chat.GetBotName()}] {botResponse}\n");
            chatLog.ScrollToEnd();

            inputBox.Clear();

            if (userInput == "EXIT")
            {
                inputBox.IsEnabled = false;
                sendButton.IsEnabled = false;
            }
        }
    }
}