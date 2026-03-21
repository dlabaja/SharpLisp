using SharpLisp.DataTypes;
using SharpLisp.Exceptions;
using System.Text.RegularExpressions;

namespace SharpLisp.Parsers;

public static class Parser
{
    private static SymbolicExpression ParseAtom(string expr)
    {
        Atom atom;
        if (long.TryParse(expr, out var num))
        {
            atom = new Atom(num, typeof(long));
        }
        else if (double.TryParse(expr, out var floatNum))
        {
            atom = new Atom(floatNum, typeof(double));
        }
        else if (expr.Equals("NIL", StringComparison.CurrentCultureIgnoreCase))
        {
            atom = Nil.NIL;
        }
        else if (expr.StartsWith('"') && expr.EndsWith('"'))
        {
            atom = new Atom(expr, typeof(string));
        }
        else
        {
            atom = new Atom(expr.ToUpper(), typeof(Symbol));
        }

        return new SymbolicExpression(atom);
    }

    private static SymbolicExpression ParseExpressionRec(string[] items, int index)
    {
        if (index > items.Length - 1)
        {
            return new SymbolicExpression(Nil.NIL);
        }

        return new SymbolicExpression(new Cons(Parse(items[0]), ParseExpressionRec(items, index + 1)));
    }
    
    private static SymbolicExpression ParseExpression(string expr)
    {
        var slicedExpr = expr.Substring(1, expr.Length - 2);
        var items = Regex.Split(slicedExpr, @"(\w|\(.+\))");
        return ParseExpressionRec(items, 0);
    }

    private static SymbolicExpression ParseSymbolicExpression(string expr)
    {
        if (IsAtom(expr))
        {
            return ParseAtom(expr);
        }

        if (IsExpresion(expr))
        {
            return ParseExpression(expr);
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
