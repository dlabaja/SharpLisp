using SharpLisp.DataTypes;
using SharpLisp.Defined;
using SharpLisp.Exceptions;
using SharpLisp.Factories;
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
            SpecialOperatorsNames.Lambda => EvaluateLambda(args, environment),
            _ => EvaluateFunction(op, args, environment)
        };
    }
    
    private static SymbolicExpression EvaluateQuote(List<SymbolicExpression> args)
    {
        FunctionUtils.CheckNumberOfArgs(SpecialOperatorsNames.Quote, args, 1);
        return args[0];
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

    private static SymbolicExpression EvaluateLambda(List<SymbolicExpression> args, Environment environment)
    {
        FunctionUtils.CheckNumberOfArgs(SpecialOperatorsNames.Lambda, args, 2);
        var lambdaArgs = ListUtils.ConsToList(args[0]);
        if (!FunctionUtils.AllSymbol(lambdaArgs))
        {
            throw new FunctionArgNotSymbolException(SpecialOperatorsNames.Lambda);
        }

        var body = args[1];
        var newEnv = new Environment(environment);
        return SymbolicExpressionFactory.Function(new Function(lambdaArgs.Select(x => x.Atom!.GetSymbol()).ToList(), body, newEnv));
    }

    public static SymbolicExpression EvaluateFunctionOp()
    {
        return SymbolicExpressionFactory.Nil;
    }
    
    public static SymbolicExpression EvaluateFuncall(List<SymbolicExpression> args, Environment environment)
    {
        var functionArg = args[0];
        if (!functionArg.IsAtom() || !functionArg.Atom.IsFunction())
        {
            throw new FunctionArgNotFunctionException(SpecialOperatorsNames.Funcall);
        }

        var function = functionArg.Atom.GetFunction();
        var funArgs = ListUtils.Mapcar(args.Cdr(), expression => Eval.EvaluateInEnv(expression, environment));
        if (funArgs.Count != function.Parameters.Count)
        {
            throw new FunctionArgCountException(SpecialOperatorsNames.Funcall, function.Parameters.Count, funArgs.Count);
        }

        for (int i = 0; i < funArgs.Count; i++)
        {
            var param = function.Parameters[i];
            var arg = funArgs[i];
            if (arg.IsAtom() && arg.Atom.IsFunction())
            {
                function.Environment.AddFunction(param.ToString(), arg.Atom.GetFunction());
                continue;
            }
            function.Environment.AddValue(param.ToString(), arg);
        }

        return Eval.EvaluateInEnv(function.Body, function.Environment);
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
