namespace SharpLisp.DataTypes;

public class Expression
{
    public SymbolicExpression Car { get; }
    public SymbolicExpression Cdr { get; }

    public Expression(SymbolicExpression car, SymbolicExpression cdr)
    {
        Car = car;
        Cdr = cdr;
    }
}
