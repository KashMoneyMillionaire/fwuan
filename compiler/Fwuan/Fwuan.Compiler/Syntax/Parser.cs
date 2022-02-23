using System;
using System.Collections.Generic;
using System.Linq;

namespace Fwuan.Compiler.Syntax
{
    internal class Parser
    {
        private readonly Queue<Token> _tokens;

        private bool IsAtEnd => !_tokens.Any();

        public Parser(IEnumerable<Token> tokens)
        {
            _tokens = new Queue<Token>(tokens);
        }

        public Expr Parse()
        {
            try
            {
                return Expression();
            }
            catch (ParseException)
            {
                return null;
            }
        }

        private Expr Expression() => Equality();

        private Expr Equality()
        {
            Expr left = Comparision();

            while (NextIsOneOf(TokenType.BangEqual, TokenType.EqualEqual))
            {
                Token op = _tokens.Dequeue();
                Expr right = Comparision();
                left = new Expr.Binary(left, op, right);
            }

            return left;
        }

        private Expr Comparision()
        {
            Expr left = Addition();

            while (NextIsOneOf(TokenType.GreaterThan, TokenType.GreaterThanEqual, TokenType.LessThan, TokenType.LessThanEqual))
            {
                Token op = _tokens.Dequeue();
                Expr right = Addition();
                left = new Expr.Binary(left, op, right);
            }

            return left;
        }

        private Expr Addition()
        {
            Expr left = Multiplication();

            while (NextIsOneOf(TokenType.Minus, TokenType.Plus))
            {
                Token op = _tokens.Dequeue();
                Expr right = Multiplication();
                left = new Expr.Binary(left, op, right);
            }

            return left;
        }

        private Expr Multiplication()
        {
            Expr left = Unary();

            while (NextIsOneOf(TokenType.Asterisk, TokenType.Slash))
            {
                Token op = _tokens.Dequeue();
                Expr right = Unary();
                left = new Expr.Binary(left, op, right);
            }

            return left;
        }

        private Expr Unary()
        {
            if (!NextIsOneOf(TokenType.Bang, TokenType.Minus))
                return Primary();

            return new Expr.Unary(_tokens.Dequeue(), Unary());
        }

        private Expr Primary()
        {
            if (NextIsOneOf(TokenType.False)) return new Expr.Literal(false);
            if (NextIsOneOf(TokenType.True)) return new Expr.Literal(true);
            if (NextIsOneOf(TokenType.Null)) return new Expr.Literal(null);

            if (NextIsOneOf(TokenType.Number, TokenType.String))
                return new Expr.Literal(_tokens.Dequeue().Literal);

            if (NextIsOneOf(TokenType.LeftParentheses))
            {
                Expr expr = Expression();
                Consume(TokenType.RightParentheses, "Expect ')' after expression.");
                return new Expr.Grouping(expr);
            }

            throw new Exception("Don't know how to handle this part 🤷‍");
        }

        private Token Consume(TokenType tokenType, string errorMessage)
        {
            Token next = _tokens.Peek();
            if (next.Type != tokenType)
                throw new Exception(errorMessage);

            return _tokens.Dequeue();
        }

        private void Synchronize()
        {
            Token current = _tokens.Dequeue();

            while (!IsAtEnd)
            {
                if (current.Type == TokenType.Semicolon)
                    return;

                Token next = _tokens.Peek();
                switch (next.Type)
                {
                    case TokenType.Class:
                    case TokenType.Function:
                    case TokenType.Var:
                    case TokenType.For:
                    case TokenType.If:
                    case TokenType.While:
                    case TokenType.Print:
                    case TokenType.Return:
                        return;
                }

                current = _tokens.Dequeue();
            }
        }

        private bool NextIsOneOf(params TokenType[] tokenTypes) => tokenTypes.Any(Check);

        private bool Check(TokenType type) => !IsAtEnd && _tokens.Peek().Type == type;
    }
}