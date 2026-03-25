namespace SharpLisp.DataTypes;

public class Primitive
{
    public int ArgCount { get; }
    public Func<SymbolicExpression[], SymbolicExpression> EvalFunction { get; }

    public Primitive(int argCount, Func<SymbolicExpression[], SymbolicExpression> function)
    {
        ArgCount = argCount;
        EvalFunction = function;
    }

    public SymbolicExpression Evaluate(SymbolicExpression[] args)
    {
        return EvalFunction(args);
    }
}
