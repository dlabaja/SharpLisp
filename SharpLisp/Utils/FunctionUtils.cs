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
