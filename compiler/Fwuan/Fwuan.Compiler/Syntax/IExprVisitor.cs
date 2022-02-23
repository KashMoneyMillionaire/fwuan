namespace Fwuan.Compiler.Syntax
{
    internal interface IExprVisitor<out T>
    {
        T VisitBinaryExpr(Expr.Binary     bin);
        T VisitGroupingExpr(Expr.Grouping @group);
        T VisitLiteralExpr(Expr.Literal   lit);
        T VisitUnaryExpr(Expr.Unary       unary);
    }
}