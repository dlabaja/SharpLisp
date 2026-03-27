using SharpLisp.DataTypes;
using SharpLisp.Exceptions;
using SharpLisp.Factories;
using SharpLisp.Utils;

namespace SharpLisp.Defined;

public static class PrimitiveFunctions
{
    public static SymbolicExpression SumPrimitive(List<SymbolicExpression> args)
    {
        return NumberFoldrPrimitive(PrimitiveNames.Sum, args, (a, b) => a + b, (a, b) => a + b, 0);
    }
    
    public static SymbolicExpression SubtractPrimitive(List<SymbolicExpression> args)
    {
        return NumberFoldrPrimitive(PrimitiveNames.Subtract, args, (a, b) => a - b, (a, b) => a - b, 0);
    }

    public static SymbolicExpression MultiplyPrimitive(List<SymbolicExpression> args)
    {
        return NumberFoldrPrimitive(PrimitiveNames.Multiply, args, (a, b) => a * b, (a, b) => a * b, 1);
    }
    
    public static SymbolicExpression DividePrimitive(List<SymbolicExpression> args)
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

        return NumberFoldrPrimitive(PrimitiveNames.Divide, args, FuncInt, FuncFloat, 1);
    }

    public static SymbolicExpression SqrtPrimitive(List<SymbolicExpression> args)
    {
        CheckNumberOfArgs(PrimitiveNames.Sqrt, args, 1);
        if (AllNumber(args))
        {
            return SymbolicExpressionFactory.Float(double.Sqrt(args[0].Atom!.GetNumber()));
        }

        throw new FunctionArgNotNumberException(PrimitiveNames.Sqrt);
    }

    public static SymbolicExpression PrintPrimitive(List<SymbolicExpression> args)
    {
        CheckNumberOfArgs(PrimitiveNames.Print, args, 1);
        var res = new List<string>();
        foreach (var arg in args)
        {
            res.Add(arg.ToStringOutput());
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Print: " + string.Join(", ", res));
        Console.ResetColor();
        return args[0];
    }
    
    private static SymbolicExpression NumberFoldrPrimitive(string funcName, List<SymbolicExpression> args, Func<long, long, long> funcInt, Func<double, double, double> funcFloat, int init)
    {
        if (!AllAtoms(args))
        {
            throw new FunctionArgNotAtomException(funcName);
        }

        if (AllInt(args))
        {
            var sub = ListUtils.Foldr(args.Select(x => x.Atom!.GetInt()).ToList(), funcInt, init);
            return SymbolicExpressionFactory.Int(sub);
        }

        if (AllNumber(args))
        {
            var sub = ListUtils.Foldr(args.Select(x => x.Atom!.GetNumber()).ToList(), funcFloat, init);
            return SymbolicExpressionFactory.Float(sub);
        }

        throw new FunctionArgNotNumberException(funcName);
    }

    private static void CheckNumberOfArgs(string funcName, List<SymbolicExpression> args, int requiredNumberOfArgs)
    {
        if (args.Count == requiredNumberOfArgs)
        {
            return;
        }

        throw new FunctionArgCountException(funcName, requiredNumberOfArgs, args.Count);
    }
    
    private static bool AllAtoms(List<SymbolicExpression> args)
    {
        return args.All(arg => arg.Atom != null);
    }
    
    private static bool AllInt(List<SymbolicExpression> args)
    {
        return args.All(arg => arg.Atom != null && arg.Atom.IsInt());
    }
    
    private static bool AllNumber(List<SymbolicExpression> args)
    {
        return args.All(arg => arg.Atom != null && (arg.Atom.IsInt() || arg.Atom.IsFloat()));
    }
}
