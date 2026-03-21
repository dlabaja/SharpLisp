namespace SharpLisp.DataTypes;

public class Cons
{
    public SymbolicExpression Car { get; }
    public SymbolicExpression Cdr { get; }

    public Cons(SymbolicExpression car, SymbolicExpression cdr)
    {
        Car = car;
        Cdr = cdr;
    }

    public override string ToString()
    {
        if (IsListCons())
        {
            return $"{Car} {Cdr}";
        }
        return $"({Car} . {Cdr})";
    }

    public bool IsListCons()
    {
        return Car.IsAtom() && (Cdr.IsCons() || Cdr.Atom?.Value.IsNil());
    }
}
