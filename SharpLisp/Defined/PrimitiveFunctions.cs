using SharpLisp.DataTypes;
using SharpLisp.Exceptions;
using SharpLisp.Factories;
using SharpLisp.Utils;

namespace SharpLisp.Defined;

public static class PrimitiveFunctions
{
    public static SymbolicExpression SumPrimitive(SymbolicExpression[] args)
    {
        return NumberFoldrPrimitive(args, (a, b) => a + b, (a, b) => a + b);
    }
    
    public static SymbolicExpression SubtractPrimitive(SymbolicExpression[] args)
    {
        return NumberFoldrPrimitive(args, (a, b) => a - b, (a, b) => a - b);
    }

    public static SymbolicExpression MultiplyPrimitive(SymbolicExpression[] args)
    {
        return NumberFoldrPrimitive(args, (a, b) => a * b, (a, b) => a * b);
    }
    
    public static SymbolicExpression DividePrimitive(SymbolicExpression[] args)
    {
        double FuncFloat(double a, double b)
        {
            if (b == 0)
            {
                throw new FunctionArgException("Cannot divide by zero");
            }
            return a / b;
        }

        long FuncInt(long a, long b)
        {
            if (b == 0)
            {
                throw new FunctionArgException("Cannot divide by zero");
            }
            return a / b;
        }

        return NumberFoldrPrimitive(args, FuncInt, FuncFloat);
    }

    public static SymbolicExpression SqrtPrimitive(SymbolicExpression[] args)
    {
        CheckNumberOfArgs(args, 1);
        if (AllNumber(args))
        {
            return SymbolicExpressionFactory.Float(double.Sqrt(args[0].Atom!.GetFloat()));
        }

        throw new FunctionArgException();
    }
    
    private static SymbolicExpression NumberFoldrPrimitive(SymbolicExpression[] args, Func<long, long, long> funcInt, Func<double, double, double> funcFloat)
    {
        if (!AllAtoms(args))
        {
            throw new FunctionArgException();
        }

        if (AllInt(args))
        {
            var sub = ListUtils.Foldr(args.Select(x => x.Atom!.GetInt()).ToList(), funcInt, 0);
            return SymbolicExpressionFactory.Int(sub);
        }

        if (AllNumber(args))
        {
            var sub = ListUtils.Foldr(args.Select(x => x.Atom!.GetNumber()).ToList(), funcFloat, 0);
            return SymbolicExpressionFactory.Float(sub);
        }

        throw new FunctionArgException();
    }

    private static void CheckNumberOfArgs(SymbolicExpression[] args, int requiredNumberOfArgs)
    {
        if (args.Length == requiredNumberOfArgs)
        {
            return;
        }

        throw new FunctionArgCountException(requiredNumberOfArgs, args.Length);
    }
    
    private static bool AllAtoms(SymbolicExpression[] args)
    {
        return args.All(arg => arg.Atom != null);
    }
    
    private static bool AllInt(SymbolicExpression[] args)
    {
        return args.All(arg => arg.Atom != null && arg.Atom.IsInt());
    }
    
    private static bool AllNumber(SymbolicExpression[] args)
    {
        return args.All(arg => arg.Atom != null && (arg.Atom.IsInt() || arg.Atom.IsFloat()));
    }
}
