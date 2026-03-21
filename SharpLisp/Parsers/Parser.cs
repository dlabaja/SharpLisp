using SharpLisp.DataTypes;
using SharpLisp.Exceptions;
using System.Text.RegularExpressions;

namespace SharpLisp.Parsers;

public static class Parser
{
    private static Atom? ParseAtom(string expr)
    {
        if (long.TryParse(expr, out var num))
        {
            return new Atom(num, typeof(long));
        }

        if (double.TryParse(expr, out var floatNum))
        {
            return new Atom(floatNum, typeof(double));
        }

        return null;
    }

    private static SymbolicExpression? ParseExpressionRec(string[] items, int index)
    {
        if (index > items.Length - 1)
        {
            return null;
        }

        return new Expression(Parse(items[0]), ParseExpressionRec(items, index + 1));
    }
    
    private static Expression? ParseExpression(string expr)
    {
        var slicedExpr = expr.Substring(1, expr.Length - 2);
        var items = Regex.Split(slicedExpr, @"(\w|\(.+\))");
        Expression? expression = null;
        Expression? current = expression;
        foreach (var item in items)
        {
            current = new 
        }
    }

    private static SymbolicExpression ParseSymbolicExpression(string expr)
    {
        if (IsAtom(expr))
        {
            ParseAtom(expr);
        }
        else if (IsExpresion(expr))
        {
            ParseExpression(expr);
        }

        throw new ParseException($"Cannot parse the sexpression {expr}");
    }

    public static SymbolicExpression Parse(string expr)
    {
        return ParseSymbolicExpression(expr.Trim());
    }
    
    private static bool IsExpresion(string expr)
    {
        return expr[0] == '(' && expr[^1] == ')';
    }

    private static bool IsAtom(string expr)
    {
        return expr[0] != '(' && expr[^1] != ')';
    }
}
