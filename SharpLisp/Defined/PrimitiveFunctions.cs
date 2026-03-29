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
        FunctionUtils.CheckNumberOfArgs(PrimitiveNames.Sqrt, args, 1);
        if (FunctionUtils.AllNumber(args))
        {
            return SymbolicExpressionFactory.Float(double.Sqrt(args[0].Atom!.GetNumber()));
        }

        throw new FunctionArgNotNumberException(PrimitiveNames.Sqrt);
    }

    public static SymbolicExpression PrintPrimitive(List<SymbolicExpression> args)
    {
        FunctionUtils.CheckNumberOfArgs(PrimitiveNames.Print, args, 1);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Print: {args[0]}");
        Console.ResetColor();
        return args[0];
    }

    public static SymbolicExpression EqlPrimitive(List<SymbolicExpression> args)
    {
        FunctionUtils.CheckNumberOfArgs(PrimitiveNames.Eql, args, 2);
        var arg1 = args[0];
        var arg2 = args[1];
        if (FunctionUtils.AllAtoms(args) && !arg1.Atom!.IsString() && !arg2.Atom!.IsString()
            && Equals(arg1.Atom.Value, arg2.Atom.Value))
        {
            return SymbolicExpressionFactory.T;
        }
        
        if (Equals(arg1, arg2))
        {
            return SymbolicExpressionFactory.T;
        }

        return SymbolicExpressionFactory.Nil;
    }
    
    public static SymbolicExpression GtPrimitive(List<SymbolicExpression> args)
    {
        FunctionUtils.CheckNumberOfArgs(PrimitiveNames.Gt, args, 2);
        if (!FunctionUtils.AllNumber(args))
        {
            throw new FunctionArgNotNumberException(PrimitiveNames.Gt);
        }
        
        if (args[0].Atom?.GetNumber() > args[1].Atom?.GetNumber())
        {
            return SymbolicExpressionFactory.T;
        }

        return SymbolicExpressionFactory.Nil;
    }

    public static SymbolicExpression ConsPrimitive(List<SymbolicExpression> args)
    {
        FunctionUtils.CheckNumberOfArgs(PrimitiveNames.Cons, args, 2);
        return SymbolicExpressionFactory.Cons(args[0], args[1]);
    }
    
    public static SymbolicExpression CarPrimitive(List<SymbolicExpression> args)
    {
        FunctionUtils.CheckNumberOfArgs(PrimitiveNames.Car, args, 1);
        var arg = args[0];
        if (arg.IsCons())
        {
            return arg.Cons.Car;
        }
        throw new FunctionArgNotConsException(PrimitiveNames.Car);
    }
    
    public static SymbolicExpression CdrPrimitive(List<SymbolicExpression> args)
    {
        FunctionUtils.CheckNumberOfArgs(PrimitiveNames.Cdr, args, 1);
        var arg = args[0];
        if (arg.IsCons())
        {
            return arg.Cons.Cdr;
        }
        throw new FunctionArgNotConsException(PrimitiveNames.Cdr);
    }

    public static SymbolicExpression ListPrimitive(List<SymbolicExpression> args)
    {
        return ListUtils.ListToCons(args);
    }
    
    public static SymbolicExpression AtompPrimitive(List<SymbolicExpression> args)
    {
        FunctionUtils.CheckNumberOfArgs(PrimitiveNames.Atomp, args, 1);
        var arg = args[0];
        return arg.IsAtom() ? SymbolicExpressionFactory.T : SymbolicExpressionFactory.Nil;
    }
    
    public static SymbolicExpression ConspPrimitive(List<SymbolicExpression> args)
    {
        FunctionUtils.CheckNumberOfArgs(PrimitiveNames.Consp, args, 1);
        var arg = args[0];
        return arg.IsCons() ? SymbolicExpressionFactory.T : SymbolicExpressionFactory.Nil;
    }
    
    public static SymbolicExpression NumberpPrimitive(List<SymbolicExpression> args)
    {
        FunctionUtils.CheckNumberOfArgs(PrimitiveNames.Numberp, args, 1);
        var arg = args[0];
        return arg.IsAtom() && arg.Atom.IsNumber() ? SymbolicExpressionFactory.T : SymbolicExpressionFactory.Nil;
    }
    
    public static SymbolicExpression SymbolpPrimitive(List<SymbolicExpression> args)
    {
        FunctionUtils.CheckNumberOfArgs(PrimitiveNames.Symbolp, args, 1);
        var arg = args[0];
        return arg.IsAtom() && arg.Atom.IsSymbol() ? SymbolicExpressionFactory.T : SymbolicExpressionFactory.Nil;
    }
    
    public static SymbolicExpression StringpPrimitive(List<SymbolicExpression> args)
    {
        FunctionUtils.CheckNumberOfArgs(PrimitiveNames.Stringp, args, 1);
        var arg = args[0];
        return arg.IsAtom() && arg.Atom.IsString() ? SymbolicExpressionFactory.T : SymbolicExpressionFactory.Nil;
    }
    
    public static SymbolicExpression FunctionpPrimitive(List<SymbolicExpression> args)
    {
        FunctionUtils.CheckNumberOfArgs(PrimitiveNames.Functionp, args, 1);
        var arg = args[0];
        return arg.IsAtom() && arg.Atom.IsFunction() ? SymbolicExpressionFactory.T : SymbolicExpressionFactory.Nil;
    }
    
    public static SymbolicExpression SymbolPrimitive(List<SymbolicExpression> args)
    {
        var result = string.Join("", args.Select(x => x.ToString()))
            .Replace("\"", "")
            .Replace("#", "")
            .Replace(".", "")
            .Replace(" ", "");
        if (string.IsNullOrWhiteSpace(result))
        {
            throw new FunctionArgException($"{PrimitiveNames.Symbol} needs at least 1 argument");
        }
        return SymbolicExpressionFactory.Symbol(result);
    }
    
    private static SymbolicExpression NumberFoldrPrimitive(string funcName, List<SymbolicExpression> args, Func<long, long, long> funcInt, Func<double, double, double> funcFloat, int init)
    {
        if (!FunctionUtils.AllAtoms(args))
        {
            throw new FunctionArgNotAtomException(funcName);
        }

        if (FunctionUtils.AllInt(args))
        {
            var sub = ListUtils.Foldr(args.Select(x => x.Atom!.GetInt()).ToList(), funcInt, init);
            return SymbolicExpressionFactory.Int(sub);
        }

        if (FunctionUtils.AllNumber(args))
        {
            var sub = ListUtils.Foldr(args.Select(x => x.Atom!.GetNumber()).ToList(), funcFloat, init);
            return SymbolicExpressionFactory.Float(sub);
        }

        throw new FunctionArgNotNumberException(funcName);
    }
}
