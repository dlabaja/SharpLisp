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

    public bool IsListCons()
    {
        return Car.IsAtom() && 
               (Cdr.IsCons() || (Cdr.Atom != null && Cdr.Atom.IsNil()));
    }

    private string ListStringRec(Cons? cons)
    {
        if (cons == null)
        {
            return "";
        }
        
        if (cons.Car.IsCons())
        {
            return $"{cons.Car.Cons!.ListString()} {ListStringRec(cons.Cdr.Cons)}";
        }

        return $"{cons.Car} {ListStringRec(cons.Cdr.Cons)}";
    }

    public string ListString()
    {
        return "(" + ListStringRec(this).Trim() + ")";
    }

    public override string ToString()
    {
        if (Cdr.Atom != null)
        {
            if (Cdr.Atom.IsNil())
            {
                return $"{Car}";
            }

            return $"{Car} . {Cdr}";
        }
        return $"{Car} {Cdr}";
    }
}
