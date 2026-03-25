namespace SharpLisp.DataTypes;

public class Function
{
    public Symbol[] Parameters { get; }
    public SymbolicExpression Body { get; }
    public Environment Environment { get; }

    public Function(Symbol[] parameters, SymbolicExpression body, Environment environment)
    {
        Parameters = parameters;
        Body = body;
        Environment = environment;
    }
}
 