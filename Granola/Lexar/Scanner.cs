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
                _tokens.Add(ScanToken());
            }

            return _tokens;
        }

        private Token ScanToken()
        {
            switch(_source[_currentIndex])
            {
                case '"':
                    return ScanString();

                default:
                    return ScanNonsense();
            }
        }

        private Token ScanString()
        {
            // skip past the first paren
            _currentIndex++;

            string comment = "";
            while (_source[_currentIndex] != '"')
            {
                comment += _source[_currentIndex];
                _currentIndex ++;

                if (IsAtEnd())
                {
                    throw new System.Exception("Improper string.");
                }
            }

            // skip past the last paren
            _currentIndex ++;

            return new Token()
            {
                Lexeme = comment,
                Type = TokenType.STRING
            };
        }

        private Token ScanNonsense()
        {
            var token = new Token()
            {
                Lexeme = "" + _source[_currentIndex]
            };

            _currentIndex++;

            return token;
        }

        private bool IsAtEnd()
        {
            return _currentIndex >= _source.Length;
        }
    }
}