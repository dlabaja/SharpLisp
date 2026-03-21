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
    
    public bool IsInt()
    {
        return Type == typeof(long);
    }

    public bool IsFloat()
    {
        return Type == typeof(double);
    }

    public bool IsString()
    {
        return Type == typeof(string);
    }

    public bool IsSymbol()
    {
        return Type == typeof(Symbol);
    }

    public bool IsNil()
    {
        return Type == typeof(Nil);
    }

    public override string ToString()
    {
        if (IsString())
        {
            return $"\"{Value}\"";
        }

        if (IsSymbol())
        {
            return Value.ToString().ToUpper();
        }
        return  Convert.ChangeType(Value, Type).ToString() ?? "";
    }
}
