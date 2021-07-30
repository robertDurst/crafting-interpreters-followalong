using System;

namespace Granola.Lexar
{
    public class Punctuation : Token
    {
        private string _punctuation;

        public Punctuation(string punctuation)
        {
            _punctuation = punctuation;
        }

        public void DisplaySelf()
        {
            Console.WriteLine(_punctuation);
        }
    }
}