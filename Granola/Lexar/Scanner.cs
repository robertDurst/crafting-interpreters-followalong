namespace Granola.Lex
{
    public class Scanner
    {
        private readonly string _source;

        private int _currentIndex;
        private int _startIndex;
        private int _currentLine;

        public Scanner(string source)
        {   
            _source = source;

            _currentIndex = 0;
            _startIndex = 0;
            _currentLine = 1;
        }

        public bool TrySkipWhitespace(char c)
        {
            bool skippedWhitespace = false;
            while (c == ' ' || c == '\r' || c == '\t' || c == '\n')
            {
                skippedWhitespace = true;
                TryIncrementNewLine();

                _currentIndex++;
                if (IsAtEnd()) break;
                c = _source[_currentIndex];
            }
            
            if (skippedWhitespace) _currentIndex --;

            return skippedWhitespace;
        }

        public char Advance() {
            return _source[_currentIndex++];
        }

        public bool Match(char expected)
        {
            if (IsAtEnd()) return false;
            if (_source[_currentIndex] != expected) return false;
            _currentIndex++;
            return true;
        }

        public char Peek() => IsAtEnd() ? '\0' : _source[_currentIndex];
        public char PeekNext() => _currentIndex + 1 >= _source.Length ? '\0' : _source[_currentIndex+1];
        public bool IsAtEnd() => _currentIndex >= _source.Length;

        public float GetFloatLiteral() => float.Parse(_source.Substring(_startIndex, _currentIndex - _startIndex));
        public string GetStringLiteral() => _source.Substring(_startIndex + 1, _currentIndex - _startIndex - 2);
        public string GetIdentifierValue() => _source.Substring(_startIndex, _currentIndex - _startIndex);

        public void TryIncrementNewLine()
        {
            if (Peek() == '\n')
            {
                    _currentLine ++;
            }
        }

        public void SetStart()
        {
            _startIndex = _currentIndex;
        }

        public Token CreateToken(TokenType type) {
            return CreateToken(type, null);
        }

        public Token CreateToken(TokenType type, object literal)
        {
            string text = _source.Substring(_startIndex, _currentIndex - _startIndex);
            return new Token()
            {
                Lexeme = text,
                Type = type,
                Literal = literal,
                Line = _currentLine,
            };
        }
    }
}