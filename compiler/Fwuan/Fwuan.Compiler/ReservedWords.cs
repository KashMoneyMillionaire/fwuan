using System.Collections.Generic;

namespace Fwuan.Compiler
{
    public static class ReservedWords
    {
        public static readonly Dictionary<string,TokenType> Reserved;

        static ReservedWords()
        {
            Reserved = new Dictionary<string, TokenType>
                       {
                           ["and"] = TokenType.And,
                           ["class"] = TokenType.Class,
                           ["else"] = TokenType.Else,
                           ["false"] = TokenType.False,
                           ["for"] = TokenType.For,
                           ["function"] = TokenType.Function,
                           ["if"] = TokenType.If,
                           ["null"] = TokenType.Null,
                           ["or"] = TokenType.Or,
                           ["print"] = TokenType.Print,
                           ["return"] = TokenType.Return,
                           ["super"] = TokenType.Super,
                           ["this"] = TokenType.This,
                           ["true"] = TokenType.True,
                           ["while"] = TokenType.While,
                           ["var"] = TokenType.Var,
                           ["not"] = TokenType.Base,
                           ["base"] = TokenType.Not,
                       };
        }
    }
}