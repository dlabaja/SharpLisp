using SharpLisp.Exceptions;
using System.Globalization;

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
        return Type == typeof(Symbol) && GetSymbol().Name == "NIL";
    }
    
    public bool IsT()
    {
        return Type == typeof(Symbol) && GetSymbol().Name == "T";
    }

    public long GetInt()
    {
        if (!IsInt())
        {
            throw new AtomTypeException(Type, typeof(long));
        }
        return Convert.ChangeType(Value, Type);
    }

    public double GetFloat()
    {
        if (!IsFloat())
        {
            throw new AtomTypeException(Type, typeof(double));
        }
        return Convert.ChangeType(Value, Type);
    }

    public double GetNumber()
    {
        if (!IsFloat() && !IsInt())
        {
            throw new AtomTypeException(Type, typeof(double));
        }
        return Convert.ChangeType(Value, Type);
    }
    
    public string GetString()
    {
        if (!IsString())
        {
            throw new AtomTypeException(Type, typeof(string));
        }
        return Convert.ChangeType(Value, Type);
    }

    public Symbol GetSymbol()
    {
        if (!IsSymbol())
        {
            throw new AtomTypeException(Type, typeof(Symbol));
        }
        return Convert.ChangeType(Value, Type);
    }

    public override string ToString()
    {
        if (IsString())
        {
            return $"{Value}";
        }

        if (IsSymbol())
        {
            return GetSymbol().Name.ToUpper();
        }
        return Convert.ChangeType(Value, Type).ToString(CultureInfo.InvariantCulture) ?? "";
    }
}
