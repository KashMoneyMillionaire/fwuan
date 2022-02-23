using System;
using Fwuan.Compiler.Syntax;

namespace Fwuan.Compiler
{
    internal class Interpreter : IExprVisitor<object>
    {
        public void Interpret(Expr expr)
        {
            object obj = expr.Visit(this);
            Console.WriteLine(Stringify(obj));
        }

        private static string Stringify(object obj) => obj?.ToString() ?? "nil";

        public object VisitLiteralExpr(Expr.Literal expr) => expr.Value;

        public object VisitGroupingExpr(Expr.Grouping expr) => expr.Expression.Visit(this);

        public object VisitUnaryExpr(Expr.Unary expr)
        {
            object right = expr.Right.Visit(this);

            switch (expr.Op.Type)
            {
                case TokenType.Minus: return -(double) right;
                case TokenType.Bang:  return !IsTruthy(right);
            }

            // Unreachable.                              
            return null;
        }

        public object VisitBinaryExpr(Expr.Binary expr)
        {
            object left = expr.Left.Visit(this);
            object right = expr.Right.Visit(this);

            if (left is double dl && right is double dr)
            {
                switch (expr.Op.Type)
                {
                    case TokenType.Plus:             return dl + dr;
                    case TokenType.Minus:            return dl - dr;
                    case TokenType.Slash:            return dl / dr;
                    case TokenType.Asterisk:         return dl * dr;
                    case TokenType.GreaterThan:      return dl > dr;
                    case TokenType.GreaterThanEqual: return dl >= dr;
                    case TokenType.LessThan:         return dl < dr;
                    case TokenType.LessThanEqual:    return dl <= dr;
                    case TokenType.EqualEqual:       return dl == dr;
                    case TokenType.BangEqual:        return dl != dr;
                }
            }

            if (left is string sl && right is string sr)
            {
                switch (expr.Op.Type)
                {
                    case TokenType.EqualEqual: return sl == sr;
                    case TokenType.BangEqual:  return sl != sr;
                    case TokenType.Plus:       return sl + sr;
                }
            }

            throw new RuntimeException($"Do not know how to apply operator ({expr.Op.Lexeme}) to {left} and {right}");
        }

        private bool IsEqual(object left, object right)
        {
            if (left == null && right == null)
                return true;

            return left?.Equals(right) == true;
        }

        private bool IsTruthy(object obj)
        {
            switch (obj)
            {
                case null:
                    return false;
                case bool b:
                    return b;
                default:
                    return true;
            }
        }
    }
}