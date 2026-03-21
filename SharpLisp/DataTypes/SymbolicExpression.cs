namespace SharpLisp.DataTypes;

public enum SymbolicExpressionType
{
    Atom,
    Expression
}

public class SymbolicExpression
{
    public SymbolicExpressionType Type { get; }
    public Atom? Atom { get; }
    public Expression? Expression { get; }

    public SymbolicExpression(Type type)
    {
        
    }
}
