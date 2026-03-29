namespace SharpLisp.DataTypes;

public class Function
{
    public string Name { get; } // hlavně pro výstup, abych věděl co je to za funkci
    public List<Symbol> Parameters { get; }
    public SymbolicExpression Body { get; }
    public Environment Environment { get; }

    public Function(string name, List<Symbol> parameters, SymbolicExpression body, Environment environment)
    {
        Name = name;
        Parameters = parameters;
        Body = body;
        Environment = environment;
    }

    public override string ToString()
    {
        return $"#FUNCTION-{Name}#";
    }
}
 