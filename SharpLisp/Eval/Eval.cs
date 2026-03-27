using SharpLisp.DataTypes;
using SharpLisp.Exceptions;

namespace SharpLisp.Eval;

public static class Eval
{
    public static SymbolicExpression EvaluateAtom(SymbolicExpression expression)
    {
        return expression;
    }
    
    public static SymbolicExpression EvaluateExpression(SymbolicExpression expression)
    {
        return expression;
    }
    
    public static SymbolicExpression Evaluate(SymbolicExpression expression)
    {
        switch (expression.Type)
        {
            case SymbolicExpressionType.Atom:
                return EvaluateAtom(expression);
            case SymbolicExpressionType.Cons:
                return EvaluateExpression(expression);
        }

        throw new EvalException(expression);
    }
}
