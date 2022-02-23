namespace Fwuan.Compiler.Syntax
{
    internal class Expr
    {
        public class Binary : Expr
        {
            public Expr  Left  { get; }
            public Expr  Right { get; }
            public Token Op    { get; }

            public Binary(Expr left, Token op, Expr right)
            {
                Left = left;
                Op = op;
                Right = right;
            }
        }

        public class Unary : Expr
        {
            public Token Op    { get; }
            public Expr  Right { get; }


            public Unary(Token op, Expr right)
            {
                Op = op;
                Right = right;
            }
        }

        public class Literal : Expr
        {
            public object Value { get; }

            public Literal(object value)
            {
                Value = value;
            }
        }

        public class Grouping : Expr
        {
            public Expr Expression { get; }

            public Grouping(Expr expression)
            {
                Expression = expression;
            }
        }
    }
}