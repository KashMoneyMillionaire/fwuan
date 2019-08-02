using System;

namespace Fwuan.Compiler
{
    internal class Token
    {
        private readonly TokenType _type;
        private readonly string _lexeme;
        private readonly object _literal;
        private readonly int _line;

        public Token(TokenType type, string lexeme, object literal, int line)
        {
            _type = type;
            _lexeme = lexeme;
            _literal = literal;
            _line = line;
        }

        public override string ToString() => $"{_type} {_lexeme} {_literal}";
    }

    public enum TokenType
    {
        // Single-Character
        LeftParentheses, RightParentheses,
        LeftBrace, RightBrace,
        Comma,
        Dot,
        Minus,
        Plus,
        Semicolon,
        Slash,
        Asterisk,
        
        // Equality
        Bang, BangEqual,
        Equal, EqualEqual,
        GreaterThan, GreaterThanEqual,
        LessThan, LessThanEqual,
        
        // Literals
        Identifier,
        String,
        Number,
        
        // Keywords
        And, Or, Else, True, False, Not,
        Class, Function, For, While, Return,
        Null, Super, Base, Var, Print,
        
        // Visibility Modifiers
//        Public, Private, Restricted,
        
        EndOfFile
    }
}