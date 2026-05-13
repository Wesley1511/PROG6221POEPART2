using System.Media;
using System.Windows;
using System.Windows.Input;

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

        private void StartChat_Click(object sender, RoutedEventArgs e)  // this function is called when the user clicks the start chat button and simply calls the BeginChat function
        {
            BeginChat();
        }

        private void nameBox_KeyDown(object sender, KeyEventArgs e)  // this function is called when the user presses a key in the name box, it checks if the key is Enter and calls the BeginChat function if it is
        {
            if (e.Key == Key.Enter)
                BeginChat();
        }

        private void BeginChat()    // this function is called when the user clicks the start chat button or presses enter in the name box, it sets the users name and displays a welcome message from the bot
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

        private void SendButton_Click(object sender, RoutedEventArgs e)  // this function is called when the user clicks the send button and simply calls the SendMessage function
        {
            SendMessage();
        }

        private void inputBox_KeyDown(object sender, KeyEventArgs e)  // this function is called when the user presses a key in the input box, it checks if the key is Enter and calls the SendMessage function if it is
        {
            if (e.Key == Key.Enter)
                SendMessage();
        }

        private void SendMessage()  // this function is called when the user clicks the send button or presses enter in the input box, it sends the users message to the bot and displays the bots response
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