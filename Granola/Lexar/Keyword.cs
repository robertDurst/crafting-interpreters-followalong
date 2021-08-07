using System.Collections.Generic;

namespace Granola.Lex
{
    public static class Keyword
    {
        public static Dictionary<string, TokenType> Keywords = new Dictionary<string, TokenType>()
        {
            {"and" , TokenType.AND},
            {"class" , TokenType.AND},
            {"end" , TokenType.AND},
            {"else" , TokenType.AND},
            {"false" , TokenType.AND},
            {"for" , TokenType.AND},
            {"fun" , TokenType.AND},
            {"if" , TokenType.AND},
            {"nul" , TokenType.AND},
            {"or" , TokenType.AND},
            {"print" , TokenType.AND},
            {"return" , TokenType.AND},
            {"super" , TokenType.AND},
            {"this" , TokenType.AND},
            {"true" , TokenType.AND},
            {"var" , TokenType.AND},
            {"while" , TokenType.AND},
        };
    }
}