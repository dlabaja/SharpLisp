using SharpLisp.DataTypes;
using SharpLisp.Exceptions;
using SharpLisp.Utils;
using Environment = SharpLisp.DataTypes.Environment;

namespace SharpLisp.Eval;

public static class EvalExpression
{
    public static SymbolicExpression EvaluateExpression(SymbolicExpression expr, Environment environment)
    {
        if (!expr.IsCons())
        {
            throw new EvalException(expr);
        }
        
        var op = expr.Cons.Car;
        if (op.IsAtom() && op.Atom.IsSymbol())
        {
            return EvaluateExp(op, expr.Cons.Cdr, environment);
        }

        throw new EvalException(expr);
    }

    private static SymbolicExpression EvaluateExp(SymbolicExpression op, SymbolicExpression args, Environment environment)
    {
        if (environment.TryGetPrimitive(op.ToString(), out var primitive))
        {
            return ApplyPrimitive(primitive, args, environment);
        }

        if (environment.TryGetFunction(op.ToString(), out var function))
        {
            return ApplyFunction(function, args, environment);
        }

        throw new EvalException();
    }

    private static SymbolicExpression ApplyPrimitive(Primitive primitive, SymbolicExpression args, Environment environment)
    {
        var argList = ListUtils.Mapcar(ListUtils.ConsToList(args), expression => Eval.EvaluateInEnv(expression, environment));
        return primitive.Evaluate(argList);
    }

    private static SymbolicExpression ApplyFunction(Function function, SymbolicExpression args, Environment environment)
    {
        // todo
        return args;
    }
}
