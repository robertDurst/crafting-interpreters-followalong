using System.Collections.Generic;

namespace Granola.Lexar
{
    public class Scanner
    {
        private readonly string _source;
        private List<Token> _tokens;

        private int _currentIndex;
        private int _startIndex;
        private int _currentLine;

        public Scanner(string source)
        {   
            _source = source;
            _tokens = new List<Token>();
        }

        public List<Token> ScanTokens()
        {
            while (!IsAtEnd())
            {
                _startIndex = _currentIndex;
                ScanToken();
            }

            _tokens.Add(new Token());
            return _tokens;
        }

        private Token ScanToken()
        {
            return new Token();
        }

        private bool IsAtEnd()
        {
            return _currentIndex >= _source.Length;
        }
    }
}