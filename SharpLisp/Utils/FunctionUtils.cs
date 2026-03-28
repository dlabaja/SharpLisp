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
}
