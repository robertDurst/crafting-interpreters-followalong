using System;
using System.IO;

namespace Granola.Lexar
{
    public class Lexar
    {
        private string _content;
        private int _contentIndex;

        public Lexar(string filepath)
        {
            try
            {
                _content = File.ReadAllText(filepath);
            }
            catch
            {
                Console.WriteLine("Unable to read file given");
            }
        }

        public bool TryGetNextToken(out Token token)
        {
            string buffer = "";

            char curChar;
            int len = 0;
            while(TryGetNextChar(out curChar))
            {
                if (Char.IsWhiteSpace(curChar))
                {
                    if (Char.IsPunctuation(curChar))
                    {
                        token = new Punctuation(curChar.ToString());
                        return true;
                    }
                    else
                    {
                        token = new Word(buffer);
                        return true;
                    }
                } else if (len != 0 && Char.IsPunctuation(curChar))
                {
                    token = new Word(buffer);
                    _contentIndex --;
                    return true;
                }
                len += 1;
                buffer += curChar;
            }

            if (len == 0 || Char.IsWhiteSpace(curChar))
            {
                token = null;
                return false;
            }
            else if (Char.IsPunctuation(curChar))
            {
                token = new Punctuation(curChar.ToString());
                return true;
            }
            else
            {
                token = new Word(buffer);
                return true;
            }
        }

        private bool TryGetNextChar(out char nextChar)
        {
            nextChar = ' ';
            if (_contentIndex >= _content.Length)
            {
                return false;
            }

            nextChar = _content[_contentIndex];
            _contentIndex++;

            return true;
        }
    }
}