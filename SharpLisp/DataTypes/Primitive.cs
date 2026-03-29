namespace SharpLisp.DataTypes;

public class Primitive
{
    public string Name { get; }
    public Func<List<SymbolicExpression>, SymbolicExpression> EvalFunction { get; }

    public Primitive(string name, Func<List<SymbolicExpression>, SymbolicExpression> function)
    {
        Name = name;
        EvalFunction = function;
    }

    public SymbolicExpression Evaluate(List<SymbolicExpression> args)
    {
        return EvalFunction(args);
    }

    public override string ToString()
    {
        return $"#FUNCTION-{Name}-PRIMITIVE#";
    }
}
