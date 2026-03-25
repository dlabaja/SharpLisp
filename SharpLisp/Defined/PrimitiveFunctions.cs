using SharpLisp.DataTypes;
using SharpLisp.Exceptions;
using SharpLisp.Factories;

namespace SharpLisp.Defined;

public static class PrimitiveFunctions
{
    public static SymbolicExpression SumPrimitive(SymbolicExpression[] args)
    {
        if (args.Length != 2)
            throw new FunctionArgException("Expected 2 arguments");

        var arg1 = args[0];
        var arg2 = args[1];
        
        if (arg1.Atom == null || arg2.Atom == null)
        {
            throw new FunctionArgException("Arg cannot be cons");
        }
        
        if (arg1.Atom.IsInt() && arg2.Atom.IsInt())
        {
            return SymbolicExpressionFactory.Int(arg1.Atom.Value + arg2.Atom.Value);
        }

        if (arg1.Atom.IsFloat() || arg2.Atom.IsFloat())
        {
            return SymbolicExpressionFactory.Float(arg1.Atom.Value + arg2.Atom.Value);
        }

        throw new FunctionArgException("Invalid arg types");
    }
    
    public static SymbolicExpression SubtractPrimitive(SymbolicExpression[] args)
    {
        if (args.Length != 2)
            throw new FunctionArgException("Expected 2 arguments");

        var arg1 = args[0];
        var arg2 = args[1];
        
        if (arg1.Atom == null || arg2.Atom == null)
        {
            throw new FunctionArgException("Arg cannot be cons");
        }
        
        if (arg1.Atom.IsInt() && arg2.Atom.IsInt())
        {
            return SymbolicExpressionFactory.Int(arg1.Atom.Value - arg2.Atom.Value);
        }

        if (arg1.Atom.IsFloat() || arg2.Atom.IsFloat())
        {
            return SymbolicExpressionFactory.Float(arg1.Atom.Value - arg2.Atom.Value);
        }

        throw new FunctionArgException("Invalid arg types");
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
