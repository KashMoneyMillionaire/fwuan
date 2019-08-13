namespace Fwuan.Compiler.Syntax
{
    internal class Expr
    {
        public class Binary : Expr
        {
            private Expr  _left;
            private Token _op;
            private Expr  _right;

            public Binary(Expr left, Token op, Expr right)
            {
                _left = left;
                _op = op;
                _right = right;
            }
        }

        public class Unary : Expr
        {
            private Token _op;
            private Expr  _right;

            public Unary(Token op, Expr right)
            {
                _op = op;
                _right = right;
            }
        }

        public class Literal : Expr
        {
            private readonly object _obj;

            public Literal(object obj)
            {
                _obj = obj;
            }
        }

        public class Grouping : Expr
        {
            private readonly Expr _expression;

            public Grouping(Expr expression)
            {
                _expression = expression;
            }
        }
    }
}