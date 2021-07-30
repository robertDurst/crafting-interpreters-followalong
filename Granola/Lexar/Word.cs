using System;

namespace Granola.Lexar
{
    public class Word : Token
    {
        private string _word;

        public Word(string word)
        {
            _word = word;
        }

        public void DisplaySelf()
        {
            Console.WriteLine(_word);
        }
    }
}