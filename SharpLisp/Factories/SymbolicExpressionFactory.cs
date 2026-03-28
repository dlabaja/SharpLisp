using SharpLisp.DataTypes;

namespace SharpLisp.Factories;

public static class SymbolicExpressionFactory
{
    public static SymbolicExpression Symbol(string name)
    {
        return new SymbolicExpression(new Atom(new Symbol(name.ToUpper()), typeof(Symbol)));
    }
    
    public static SymbolicExpression Int(long value)
    {
        return new SymbolicExpression(new Atom(value, typeof(long)));
    }
    
    public static SymbolicExpression Float(double value)
    {
        return new SymbolicExpression(new Atom(value, typeof(double)));
    }
    
    public static SymbolicExpression String(string content)
    {
        return new SymbolicExpression(new Atom(content, typeof(string)));
    }

    public static SymbolicExpression Nil { get; } = new SymbolicExpression(new Atom(new Symbol("NIL"), typeof(Symbol)));
    
    public static SymbolicExpression T { get; } = new SymbolicExpression(new Atom(new Symbol("T"), typeof(Symbol)));

}
