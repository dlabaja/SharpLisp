using SharpLisp.DataTypes;
using SharpLisp.Exceptions;
using Environment = SharpLisp.DataTypes.Environment;

namespace SharpLisp.Eval;

public static class EvalAtom
{
    public static SymbolicExpression EvaluateAtom(SymbolicExpression expr, Environment environment)
    {
        if (expr.IsAtom() && expr.Atom.IsSymbol())
        {
            environment.TryGetValue(expr.Atom.ToString(), out var value);
            return value ?? throw new ValueNotFoundException(expr.Atom.ToString());
        }
        return expr;
    }
}
