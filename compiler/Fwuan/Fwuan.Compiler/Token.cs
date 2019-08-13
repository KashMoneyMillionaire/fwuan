using System;

namespace Fwuan.Compiler
{
    internal class Token
    {
        private readonly string _lexeme;
        private readonly int    _line;

        public object    Literal { get; }
        public  TokenType Type    { get; }

        public Token(TokenType type, string lexeme, object literal, int line)
        {
            Type = type;
            _lexeme = lexeme;
            Literal = literal;
            _line = line;
        }

        public override string ToString() => $"{Type} {_lexeme} {Literal}";
    }

    public enum TokenType
    {
        // Single-Character
        LeftParentheses,
        RightParentheses,
        LeftBrace,
        RightBrace,
        Comma,
        Dot,
        Minus,
        Plus,
        Semicolon,
        Slash,
        Asterisk,

        // Equality
        Bang,
        BangEqual,
        Equal,
        EqualEqual,
        GreaterThan,
        GreaterThanEqual,
        LessThan,
        LessThanEqual,

        // Literals
        Identifier,
        String,
        Number,

        // Keywords
        And,
        Or,
        Else,
        True,
        False,
        Not,
        If,
        Class,
        Function,
        For,
        While,
        Return,
        Null,
        Super,
        Base,
        This,
        Var,
        Print,

        // Visibility Modifiers
//        Public, Private, Restricted,

        EndOfFile
    }
}