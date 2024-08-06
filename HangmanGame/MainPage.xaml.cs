using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Xml.Serialization;

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
            ChooseWord();
            CalculateWord(response, guess);
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

        private void ChooseWord()
        {
            response = words[new Random().Next(0, words.Count)];
            Debug.WriteLine(response);
        }

        private void CalculateWord(string response, List<char> guess)
        {
            char[] result = response.Select(x => (guess.IndexOf(x) >= 0 ? x : '_')).ToArray();
            Destaque = string.Join(' ', result);
        }

        private void TreatHunch(char letter)
        {
            if (guess.IndexOf(letter) == -1)
                guess.Add(letter);

            if (response.IndexOf(letter) >= 0)
            {
                CalculateWord(response, guess);
                CheckWon();
            }
            else if(response.IndexOf(letter) == -1)
            {
                erros++;
                UpdateStatus();
                CheckLoss();
                CurrentImage = $"game_img{erros}.png";
            }
                
        }

        private void CheckWon() 
        {
            if (Destaque.Replace(" ", "") == response)
            {
                Message = "Você ganhou!!!";
                DisableLetters();
            }
        }
        private void CheckLoss()
        {
            if (erros == maxErros)
            {
                Message = "Você perdeu!";
                DisableLetters();
            }
        }

        private void UpdateStatus()
        {
            GameStatus = $"Erros: {erros} de {maxErros}";
        }

        private void DisableLetters()
        {
            foreach (var children in conteinerLetters.Children)
            {
                var btn = children as Button;

                if (btn != null)
                    btn.IsEnabled = false;
            }
        }

        private void EnableLetters()
        {
            foreach (var children in conteinerLetters.Children)
            {
                var btn = children as Button;

                if (btn != null)
                    btn.IsEnabled = true;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;

            if (btn != null)
            {
                var letter = btn.Text;
                btn.IsEnabled = false;
                TreatHunch(letter[0]);
            }
        }

        private void ResetButton_Clicked(object sender, EventArgs e)
        {
            erros = 0;
            guess = new List<char>();
            CurrentImage = "game_img0.png";
            ChooseWord();
            CalculateWord(response, guess); //do the test
            Message = "";
            UpdateStatus();
            EnableLetters();
        }
    }
}
