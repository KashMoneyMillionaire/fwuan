namespace Fwuan.Compiler.Syntax
{
    internal static class ExprExtensions
    {
        public static T Visit<T>(this Expr expr, IExprVisitor<T> visitor)
        {
            switch (expr)
            {
                case Expr.Binary bin:     return visitor.VisitBinaryExpr(bin);
                case Expr.Grouping group: return visitor.VisitGroupingExpr(group);
                case Expr.Literal lit:    return visitor.VisitLiteralExpr(lit);
                case Expr.Unary unary:    return visitor.VisitUnaryExpr(unary);
            }

            return default;
        }
    }
}