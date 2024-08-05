using System.ComponentModel;

namespace HangmanGame
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private string _destaque;
        private List<char> _letters = new();
        private string _message;
        private string _gameStatus;
        private string _currentImage = "game_img0.png";
        string response = "";
        int erros = 0;
        int maxErros = 6;
        List<char> guess = new();
        List<string> words = new List<string>()
        { 
            "dotnet",
            "maui",
            "aspnet",
            "sql",
            "python",
            "php",
            "javascript",
            "r",
            "ruby",
            "mvvm",
            "oop",
            "vscode",
        };

        public MainPage()
        {
            InitializeComponent();
            Letters.AddRange("abcdefghijklmnopqrstuvwxyz");
            GameStatus = $"Erros: {erros} de {maxErros}";
            BindingContext = this;
        }

        public string Destaque
        {
            get => _destaque;
            set
            {
                _destaque = value;
                OnPropertyChanged();
            }
        }

        public List<char> Letters
        {
            get => _letters;
            set
            {
                _letters = value;
                OnPropertyChanged();
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public string GameStatus
        {
            get => _gameStatus;
            set
            {
                _gameStatus = value;
                OnPropertyChanged();
            }
        }

        public string CurrentImage
        {
            get => _currentImage;
            set 
            {
                _currentImage = value;
                OnPropertyChanged();
            }
        }

        private void ResetButton_Clicked(object sender, EventArgs e)
        {

        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}
