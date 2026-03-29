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
            SpecialOperatorsNames.Function => EvaluateFunction(args, environment),
            SpecialOperatorsNames.Funcall => EvaluateFuncall(args, environment),
            SpecialOperatorsNames.Defun => EvaluateDefun(args),
            SpecialOperatorsNames.Defmacro => EvaluateDefmacro(args),
            SpecialOperatorsNames.Labels => EvaluateLabels(args, environment),
             SpecialOperatorsNames.Error => EvaluateError(args), 
            _ => EvaluateOperator(op, args, environment)
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

    private static SymbolicExpression EvaluateFunction(List<SymbolicExpression> args, Environment environment)
    {
        FunctionUtils.CheckNumberOfArgs(SpecialOperatorsNames.Function, args, 1);
        var symbolName = FunctionUtils.SymbolArg(SpecialOperatorsNames.Function, Eval.Evaluate(args[0])).Name;
        if (environment.TryGetPrimitive(symbolName, out var primitive))
        {
            return SymbolicExpressionFactory.Primitive(primitive);
        }
        if (environment.TryGetFunction(symbolName, out var function))
        {
            return SymbolicExpressionFactory.Function(function);
        }

        throw new FunctionNotFoundException(symbolName);
    }

    private static SymbolicExpression EvaluateFuncall(List<SymbolicExpression> args, Environment environment)
    {
        var functionArg = Eval.EvaluateInEnv(args[0], environment);
        var rest = ListUtils.Mapcar(args.Cdr(), expression => Eval.EvaluateInEnv(expression, environment));
        if (!functionArg.IsAtom())
        {
            throw new FunctionArgNotFunctionException(SpecialOperatorsNames.Funcall);
        }

        if (functionArg.Atom.IsPrimitive())
        {
            return ApplyPrimitive(functionArg.Atom.GetPrimitive(), rest);
        }

        if (functionArg.Atom.IsFunction())
        {
            return ApplyFunction(functionArg.Atom.GetFunction(), rest);
        }
        
        throw new FunctionArgNotFunctionException(SpecialOperatorsNames.Funcall);
    }

    private static SymbolicExpression EvaluateDefun(List<SymbolicExpression> args)
    {
        FunctionUtils.CheckNumberOfArgs(SpecialOperatorsNames.Defun, args, 3);
        var name = FunctionUtils.SymbolArg(SpecialOperatorsNames.Defun, args[0]).Name;
        var funcParams = ListUtils.ConsToList(args[1]);
        var body = args[2];
        if (!FunctionUtils.AllSymbol(funcParams))
        {
            throw new FunctionArgNotSymbolException(SpecialOperatorsNames.Defun);
        }

        var function = new Function(funcParams.Select(x => x.Atom!.GetSymbol()).ToList(), body, GlobalEnvironment.Environment);
        GlobalEnvironment.Environment.AddFunction(name, function);
        return SymbolicExpressionFactory.Function(function);
    }
    
    private static SymbolicExpression EvaluateDefmacro(List<SymbolicExpression> args)
    {
        FunctionUtils.CheckNumberOfArgs(SpecialOperatorsNames.Defmacro, args, 3);
        var name = FunctionUtils.SymbolArg(SpecialOperatorsNames.Defmacro, args[0]).Name;
        var macroParams = ListUtils.ConsToList(args[1]);
        var body = args[2];
        if (!FunctionUtils.AllSymbol(macroParams))
        {
            throw new FunctionArgNotSymbolException(SpecialOperatorsNames.Defmacro);
        }

        var macro = new Macro(new Function(macroParams.Select(x => x.Atom!.GetSymbol()).ToList(), body, GlobalEnvironment.Environment));
        GlobalEnvironment.Environment.AddMacro(name, macro);
        return SymbolicExpressionFactory.Macro(macro);
    }
    
    private static SymbolicExpression EvaluateLabels(List<SymbolicExpression> args, Environment environment)
    {
        FunctionUtils.CheckNumberOfArgs(SpecialOperatorsNames.Labels, args, 2);
        var env = new Environment(environment);
        var funcs = ListUtils.ConsToList(args[0]);
        foreach (var func in funcs)
        {
            var items = ListUtils.ConsToList(func);
            var name = FunctionUtils.SymbolArg(SpecialOperatorsNames.Labels, items[0]);
            var funcParams = ListUtils.ConsToList(items[1]);
            var body = items[2];
            if (!FunctionUtils.AllSymbol(funcParams))
            {
                throw new FunctionArgNotSymbolException(SpecialOperatorsNames.Labels);
            }
            env.AddFunction(name.Name, new Function(funcParams.Select(x => x.Atom!.GetSymbol()).ToList(), body, environment));
        }

        return Eval.EvaluateInEnv(args[1], env);
    }

    private static SymbolicExpression EvaluateError(List<SymbolicExpression> args)
    {
        throw new UserException(string.Join(' ', args));
    }
    
    private static SymbolicExpression EvaluateOperator(string op, List<SymbolicExpression> args, Environment environment)
    {
        if (environment.TryGetMacro(op, out var macro))
        {
            return Eval.EvaluateInEnv(ApplyFunction(macro.Expander, args), environment);
        }
        var evaluatedArgs = ListUtils.Mapcar(args, expression => Eval.EvaluateInEnv(expression, environment));
        if (environment.TryGetPrimitive(op, out var primitive))
        {
            return ApplyPrimitive(primitive, evaluatedArgs);
        }
        if (environment.TryGetFunction(op, out var function))
        {
            return ApplyFunction(function, evaluatedArgs);
        }

        throw new FunctionNotFoundException(op);
    }

    private static SymbolicExpression ApplyPrimitive(Primitive primitive, List<SymbolicExpression> args)
    {
        return primitive.Evaluate(args);
    }

    private static SymbolicExpression ApplyFunction(Function function, List<SymbolicExpression> args)
    {
        var env = new Environment(function.Environment);
        for (int i = 0; i < args.Count; i++)
        {
            var param = function.Parameters[i];
            var arg = args[i];
            if (arg.IsAtom() && arg.Atom.IsFunction())
            {
                env.AddFunction(param.ToString(), arg.Atom.GetFunction());
                continue;
            }
            env.AddValue(param.ToString(), arg);
        }

        return Eval.EvaluateInEnv(function.Body, env);
    }
}
