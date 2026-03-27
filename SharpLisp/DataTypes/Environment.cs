using SharpLisp.Exceptions;

namespace SharpLisp.DataTypes;

public class Environment
{
    public Environment? Parent { get; }
    private readonly Dictionary<string, Function> _functions = new Dictionary<string, Function>();
    private readonly Dictionary<string, Primitive> _primitives = new Dictionary<string, Primitive>();
    private readonly Dictionary<string, SymbolicExpression> _values = new Dictionary<string, SymbolicExpression>();

    public Environment(Environment? parent)
    {
        Parent = parent;
    }
    
    public void AddPrimitive(string name, Primitive primitive)
    {
        _primitives.TryAdd(name, primitive);
    }

    public void AddFunction(string name, Function function)
    {
        _functions.TryAdd(name, function);
    }

    public void AddValue(string name, SymbolicExpression expr)
    {
        _values.TryAdd(name, expr);
    }
    
    public Primitive GetPrimitive(string name)
    {
        if (_primitives.TryGetValue(name, out Primitive? value))
        {
            return value;
        }

        if (Parent != null)
        {
            return Parent.GetPrimitive(name);
        }

        throw new FunctionNotFoundException($"Cannot find primitive {name}");
    }

    public Function GetFunction(string name)
    {
        if (_functions.TryGetValue(name, out Function? value))
        {
            return value;
        }

        if (Parent != null)
        {
            return Parent.GetFunction(name);
        }

        throw new FunctionNotFoundException($"Cannot find function {name}");
    }

    public SymbolicExpression GetValue(string name)
    {
        if (_values.TryGetValue(name, out SymbolicExpression? value))
        {
            return value;
        }

        if (Parent != null)
        {
            return Parent.GetValue(name);
        }

        throw new ValueNotFoundException(name);
    }
}
