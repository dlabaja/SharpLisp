using SharpLisp.DataTypes;
using SharpLisp.Factories;
using System.Globalization;

namespace SharpLisp.Parsers;

public static class AtomParser
{
    public static SymbolicExpression ParseAtom(string expr)
    {
        SymbolicExpression atom;
        if (double.TryParse(expr, CultureInfo.InvariantCulture, out var floatNum))
        {
            atom = SymbolicExpressionFactory.Float(floatNum);
        }
        else if (long.TryParse(expr, out var num))
        {
            atom = SymbolicExpressionFactory.Int(num);
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
