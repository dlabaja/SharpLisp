using SharpLisp.DataTypes;
using SharpLisp.Defined;
using SharpLisp.Exceptions;
using Environment = SharpLisp.DataTypes.Environment;

namespace SharpLisp.Eval;

public static class Eval
{
    public static SymbolicExpression Evaluate(SymbolicExpression expression)
    {
        return EvaluateInEnv(expression, GlobalEnvironment.Environment);
    }

    public static SymbolicExpression EvaluateInEnv(SymbolicExpression expression, Environment environment)
    {
        switch (expression.Type)
        {
            case SymbolicExpressionType.Atom:
                return EvalAtom.EvaluateAtom(expression, environment);
            case SymbolicExpressionType.Cons:
                return EvalExpression.EvaluateExpression(expression, environment);
        }

        throw new EvalException(expression);
    }
}
