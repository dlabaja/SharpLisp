using SharpLisp.DataTypes;

namespace SharpLisp.Parsers;

public static class AtomParser
{
    public static SymbolicExpression ParseAtom(string expr)
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
}
