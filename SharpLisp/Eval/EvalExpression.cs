using SharpLisp.DataTypes;
using SharpLisp.Defined;
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
            return EvaluateExp(op.ToString(), ListUtils.ConsToList(expr.Cons.Cdr), environment);
        }

        throw new EvalException(expr);
    }

    private static SymbolicExpression EvaluateExp(string op, List<SymbolicExpression> args, Environment environment)
    {
        return op switch
        {
            SpecialOperatorsNames.Quote => EvaluateQuote(args),
            SpecialOperatorsNames.If => EvaluateIf(args, environment),
            _ => EvaluateFunction(op, args, environment)
        };
    }

    private static SymbolicExpression EvaluateIf(List<SymbolicExpression> args, Environment environment)
    {
        FunctionUtils.CheckNumberOfArgs(SpecialOperatorsNames.If, args, 3);
        var test = Eval.EvaluateInEnv(args[0], environment);
        var a = args[1];
        var b = args[2];
        if (test.IsAtom() && test.Atom.IsNil())
        {
            return Eval.EvaluateInEnv(b, environment);
        }

        return Eval.EvaluateInEnv(a, environment);
    }
    
    private static SymbolicExpression EvaluateQuote(List<SymbolicExpression> args)
    {
        FunctionUtils.CheckNumberOfArgs(SpecialOperatorsNames.Quote, args, 1);
        return args[0];
    }

    private static SymbolicExpression EvaluateFunction(string op, List<SymbolicExpression> args, Environment environment)
    {
        if (environment.TryGetPrimitive(op, out var primitive))
        {
            return ApplyPrimitive(primitive, args, environment);
        }
        if (environment.TryGetFunction(op, out var function))
        {
            return ApplyFunction(function, args, environment);
        }

        throw new FunctionNotFoundException(op);
    }

    private static SymbolicExpression ApplyPrimitive(Primitive primitive, List<SymbolicExpression> args, Environment environment)
    {
        var argList = ListUtils.Mapcar(args, expression => Eval.EvaluateInEnv(expression, environment));
        return primitive.Evaluate(argList);
    }

    private static SymbolicExpression ApplyFunction(Function function, List<SymbolicExpression> args, Environment environment)
    {
        // todo
        return args[0];
    }
}
