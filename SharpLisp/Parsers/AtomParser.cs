using SharpLisp.DataTypes;
using SharpLisp.Factories;

namespace SharpLisp.Parsers;

public static class AtomParser
{
    public static SymbolicExpression ParseAtom(string expr)
    {
        SymbolicExpression atom;
        if (long.TryParse(expr, out var num))
        {
            atom = SymbolicExpressionFactory.Int(num);
        }
        else if (double.TryParse(expr, out var floatNum))
        {
            atom = SymbolicExpressionFactory.Float(num);
        }
        else if (expr.Equals("NIL", StringComparison.CurrentCultureIgnoreCase))
        {
            atom = SymbolicExpressionFactory.Nil;
        }
        else if (expr.StartsWith('"') && expr.EndsWith('"'))
        {
            atom = SymbolicExpressionFactory.String(expr);
        }
        else
        {
            atom = SymbolicExpressionFactory.Symbol(expr);
        }

        return atom;
    }
}
