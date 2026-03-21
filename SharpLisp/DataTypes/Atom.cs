namespace SharpLisp.DataTypes;

public class Atom
{
    public dynamic Value { get; }
    public Type Type { get; }

    public Atom(dynamic value, Type type)
    {
        Value = value;
        Type = type;
    }
}
