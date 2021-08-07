using System.Collections.Generic;

namespace Granola.Lex
{
    public class Lexar
    {
        private List<Token> _tokens;
        private Scanner _scanner;


        public Lexar(string source)
        {   
            _scanner = new Scanner(source);
            _tokens = new List<Token>();
        }

        public List<Token> ScanTokens()
        {
            while (!_scanner.IsAtEnd())
            {
                _scanner.SetStart();

                var curChar = _scanner.Advance();
                if (_scanner.TrySkipWhitespace(curChar))
                {
                    continue;
                }
                _tokens.Add(ScanToken(curChar));
            }

            _tokens.Add(_scanner.CreateToken(TokenType.EOF));
            return _tokens;
        }

        private Token ScanToken(char c)
            => (c) switch
            {
                // single char tokens
                '(' => _scanner.CreateToken(TokenType.LEFT_PAREN),
                ')' => _scanner.CreateToken(TokenType.RIGHT_PAREN),
                '{' => _scanner.CreateToken(TokenType.LEFT_BRACE),
                '}' => _scanner.CreateToken(TokenType.RIGHT_BRACE),
                ',' => _scanner.CreateToken(TokenType.COMMA),
                '.' => _scanner.CreateToken(TokenType.DOT),
                '-' => _scanner.CreateToken(TokenType.MINUS),
                '+' => _scanner.CreateToken(TokenType.PLUS),
                ';' => _scanner.CreateToken(TokenType.SEMICOLON),
                '*' => _scanner.CreateToken(TokenType.STAR),

                // single/double char tokens
                '!' => _scanner.CreateToken(_scanner.Match('=') ? TokenType.BANG_EQUAL : TokenType.BANG),
                '=' => _scanner.CreateToken(_scanner.Match('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL),
                '<' => _scanner.CreateToken(_scanner.Match('=') ? TokenType.LESS_EQUAL : TokenType.LESS),
                '>' => _scanner.CreateToken(_scanner.Match('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER),

                // longer tokens
                '/' => ScanTokenSlash(),
                '"' => ScanTokenString(),

                _   => ScanTokenDefault(c),
                    
            };

        private Token ScanTokenDefault(char c)
        {
            if (char.IsDigit(c))
            {
                return ScanTokenNumber();
            }
            else if (char.IsLetter(c) || c == '_')
            {
                return ScanIdentifier();
            }

            throw new System.Exception("Unexpected token");
        }

        private Token ScanTokenSlash()
        {
            if (_scanner.Match('/'))
            {
                while(_scanner.Peek() != '\n' && !_scanner.IsAtEnd()) _scanner.Advance();
                return _scanner.CreateToken(TokenType.SINGLE_LINE_COMMENT);
            }
            else 
            {
                return _scanner.CreateToken(TokenType.SLASH);
            }
        }

        // allows for multi-line strings
        private Token ScanTokenString()
        {
            while(_scanner.Peek() != '"' && !_scanner.IsAtEnd())
            {
                _scanner.TryIncrementNewLine();
                _scanner.Advance();
            }

            if (_scanner.IsAtEnd())
            {
                throw new System.Exception("Unterminated string!");
            }

            // closing '"'
            _scanner.Advance();

            // trim the surrounding quotes
            string value = _scanner.GetStringLiteral();
            return _scanner.CreateToken(TokenType.STRING, value);
        }

        // all numbers are floats, leading and trailing dots not allowed
        private Token ScanTokenNumber()
        {
            while(char.IsDigit(_scanner.Peek()))
            {
                _scanner.Advance();
            }

            // fraction
            if (_scanner.Peek() == '.' && char.IsDigit(_scanner.PeekNext()))
            {
                // consume the '.'
                _scanner.Advance();
                while(char.IsDigit(_scanner.Peek()))
                {
                    _scanner.Advance();
                }
            }

            return _scanner.CreateToken(TokenType.NUMBER, _scanner.GetFloatLiteral());
        }

        private Token ScanIdentifier()
        {
            while(char.IsLetterOrDigit(_scanner.Peek()))
            {
                _scanner.Advance();
            }

            string identifier = _scanner.GetIdentifierValue();
            TokenType type = TokenType.IDENTIFIER;
            if (Keyword.Keywords.ContainsKey(identifier))
            {
                type = Keyword.Keywords[identifier];
            }

            return _scanner.CreateToken(type);
        }
    }
}