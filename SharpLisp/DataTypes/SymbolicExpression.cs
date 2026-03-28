using System.Diagnostics.CodeAnalysis;

namespace SharpLisp.DataTypes;

public enum SymbolicExpressionType
{
    Atom,
    Cons
}

public class SymbolicExpression
{
    public SymbolicExpressionType Type { get; }
    public Atom? Atom { get; }
    public Cons? Cons { get; }

    public SymbolicExpression(Atom atom)
    {
        Type = SymbolicExpressionType.Atom;
        Atom = atom;
    }
    
    public SymbolicExpression(Cons cons)
    {
        Type = SymbolicExpressionType.Cons;
        Cons = cons;
    }

    [MemberNotNullWhen(true, nameof(Atom))]
    public bool IsAtom()
    {
        return Type == SymbolicExpressionType.Atom && Atom != null;
    }

    [MemberNotNullWhen(true, nameof(Cons))]
    public bool IsCons()
    {
        return Type == SymbolicExpressionType.Cons && Cons != null;
    }

    public override string ToString()
    {
        switch (Type)
        {
            case SymbolicExpressionType.Atom:
                return Atom?.ToString() ?? "";
            case SymbolicExpressionType.Cons:
                return Cons?.ToString() ?? "";
        }
        return "";
    }

    public string ToStringOutput()
    {
        if (IsAtom())
        {
            return Atom.ToString();
        }
        return Cons!.ToString();   
    }
}
