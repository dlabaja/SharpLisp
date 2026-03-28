namespace SharpLisp.DataTypes;

public class Primitive
{
    public Func<List<SymbolicExpression>, SymbolicExpression> EvalFunction { get; }

    public Primitive(Func<List<SymbolicExpression>, SymbolicExpression> function)
    {
        EvalFunction = function;
    }

    public SymbolicExpression Evaluate(List<SymbolicExpression> args)
    {
        return EvalFunction(args);
    }

    public override string ToString()
    {
        return "#FUNCTION-PRIMITIVE#";
    }
}
