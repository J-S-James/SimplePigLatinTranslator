using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimplePigLatinTranslator
{
    public class AppViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private string _userInput;
        private string _translatedString;

        public AppViewModel() 
        { 
            UserInput = "simple pig latin translator";
        }

        public string UserInput
        {
            get { return _userInput;  }
            set
            {
                _userInput = value;
                OnPropertyChanged();
                TranslatedString = Translater(UserInput);
            }
        }

        public string TranslatedString
        {
            get { return _translatedString; }
            set
            {
                _translatedString = value;
                OnPropertyChanged();
            }
        }

        private string Translater(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            var sb = new StringBuilder();
            char[] delimiterChars = { ' ', ',', '.', ':' };
            string[] words = input.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words) 
            {
                sb.Append(WordTranslater(word) + " ");
            }

            return sb.ToString();
        }

        private string WordTranslater(string word)
        {
            var vowels = "aeiouAEIOU";

            if (vowels.Contains(word[0])) return WordBuilder(word.ToLower(), 0);

            for (int i = 1; i < word.Length; i++)
            {
                if (vowels.Contains(word[i]) || "yY".Contains(word[i]))
                {
                    return WordBuilder(word.ToLower(), i);
                }
            }

            return word;
        }

        private string WordBuilder(string word, int vowelPlacement)
        {
            var sb = new StringBuilder();

            if (vowelPlacement == 0)
            {
                sb.Append(word + "yay");
            }
            else
            {
                sb.Append(word[vowelPlacement..] + word.Substring(0, vowelPlacement) + "ay");

            }

            return sb.ToString();
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
