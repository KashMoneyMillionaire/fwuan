using System;
using System.Text;
using Fwuan.Compiler.Syntax;

namespace Fwuan.Compiler
{
    internal class AtsPrinter : IExprVisitor<string>
    {
        public void Print(Expr expr)
        {
            string ats = expr.Visit(this);
            Console.WriteLine(ats);
        }

        public string VisitBinaryExpr(Expr.Binary expr) => Parenthesize(expr.Op.Lexeme, expr.Left, expr.Right);

        public string VisitGroupingExpr(Expr.Grouping expr) => Parenthesize("group", expr.Expression);

        public string VisitLiteralExpr(Expr.Literal expr) => expr.Value?.ToString() ?? "null";

        public string VisitUnaryExpr(Expr.Unary expr) => Parenthesize(expr.Op.Lexeme, expr.Right);

        private string Parenthesize(string name, params Expr[] expressions)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("(").Append(name);
            foreach (Expr expr in expressions)
            {
                builder.Append(" ");
                builder.Append(expr.Visit(this));
            }

            builder.Append(")");

            return builder.ToString();
        }
    }
}