using SharpLisp.DataTypes;
using SharpLisp.Exceptions;

namespace SharpLisp.Utils;

public static class FunctionUtils
{
    public static void CheckNumberOfArgs(string funcName, List<SymbolicExpression> args, int requiredNumberOfArgs)
    {
        if (args.Count == requiredNumberOfArgs)
        {
            return;
        }

        throw new FunctionArgCountException(funcName, requiredNumberOfArgs, args.Count);
    }

    public static Symbol SymbolArg(string funcName, SymbolicExpression expr, bool omitBools = true)
    {
        if (!expr.IsAtom() || !expr.Atom.IsSymbol())
        {
            throw new FunctionArgNotSymbolException(funcName);
        }

        if (omitBools && (expr.Atom.IsT() || expr.Atom.IsNil()))
        {
            throw new FunctionArgNotSymbolException(funcName);
        }
        
        return expr.Atom.GetSymbol();
    }
    
    public static Function FunctionArg(string funcName, SymbolicExpression expr)
    {
        if (!expr.IsAtom() || !expr.Atom.IsFunction())
        {
            throw new FunctionArgNotFunctionException(funcName);
        }
        
        return expr.Atom.GetFunction();
    }
    
    public static Primitive PrimitiveArg(string funcName, SymbolicExpression expr)
    {
        if (!expr.IsAtom() || !expr.Atom.IsPrimitive())
        {
            throw new FunctionArgNotFunctionException(funcName);
        }
        
        return expr.Atom.GetPrimitive();
    }
    
    public static bool AllAtoms(List<SymbolicExpression> args)
    {
        return args.All(arg => arg.IsAtom());
    }
    
    public static bool AllInt(List<SymbolicExpression> args)
    {
        return args.All(arg => arg.IsAtom() && arg.Atom.IsInt());
    }
    
    public static bool AllNumber(List<SymbolicExpression> args)
    {
        return args.All(arg => arg.IsAtom() && (arg.Atom.IsInt() || arg.Atom.IsFloat()));
    }
    
    public static bool AllSymbol(List<SymbolicExpression> args)
    {
        return args.All(arg => arg.IsAtom() && arg.Atom.IsSymbol());
    }
}
