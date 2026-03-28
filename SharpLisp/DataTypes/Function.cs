namespace SharpLisp.DataTypes;

public class Function
{
    public List<Symbol> Parameters { get; }
    public SymbolicExpression Body { get; }
    public Environment Environment { get; }

    public Function(List<Symbol> parameters, SymbolicExpression body, Environment environment)
    {
        Parameters = parameters;
        Body = body;
        Environment = environment;
    }

    public override string ToString()
    {
        return "#FUNCTION";
    }
}
 