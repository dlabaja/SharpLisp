namespace SharpLisp.DataTypes;

public class Primitive
{
    public Func<SymbolicExpression[], SymbolicExpression> EvalFunction { get; }

    public Primitive(Func<SymbolicExpression[], SymbolicExpression> function)
    {
        EvalFunction = function;
    }

    public SymbolicExpression Evaluate(SymbolicExpression[] args)
    {
        return EvalFunction(args);
    }
}
