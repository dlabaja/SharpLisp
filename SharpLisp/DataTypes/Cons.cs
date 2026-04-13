namespace SharpLisp.DataTypes;

public class Cons
{
    public SymbolicExpression Car { get; set; }
    public SymbolicExpression Cdr { get; set; }

    public Cons(SymbolicExpression car, SymbolicExpression cdr)
    {
        Car = car;
        Cdr = cdr;
    }

    private Atom GetLastCdr()
    {
        if (Cdr.IsAtom())
        {
            return Cdr.Atom;
        }

        return Cdr.Cons!.GetLastCdr();
    }

    private string PrintPureList()
    {
        if (Cdr.IsAtom())
        {
            return $"{Car}";
        }

        return $"{Car} {Cdr.Cons!.PrintPureList()}";;
    }
    
    private string PrintDotList()
    {
        if (Cdr.IsAtom())
        {
            return $"{Car} . {Cdr}";
        }

        return $"{Car} {Cdr.Cons!.PrintDotList()}";
    }
    
    public override string ToString()
    {
        var lastCdr = GetLastCdr();
        string content;
        if (lastCdr.IsNil())
        {
            content = PrintPureList();
        }
        else
        {
            content = PrintDotList();
        }
        return "(" + content.Trim() + ")";
    }
}
