namespace SharpLisp.DataTypes;

public class Environment
{
    public Environment? Parent { get; }
    private readonly Dictionary<string, Macro> _macros = new Dictionary<string, Macro>();
    private readonly Dictionary<string, Function> _functions = new Dictionary<string, Function>();
    private readonly Dictionary<string, Primitive> _primitives = new Dictionary<string, Primitive>();
    private readonly Dictionary<string, SymbolicExpression> _values = new Dictionary<string, SymbolicExpression>();

    public Environment(Environment? parent)
    {
        Parent = parent;
    }

    public List<string> GetMacroNames()
    {
        return _macros.Keys.ToList();
    }
    
    public List<string> GetFunctionNames()
    {
        return _functions.Keys.ToList();
    }
    
    public List<string> GetPrimitiveNames()
    {
        return _primitives.Keys.ToList();
    }
    
    public List<string> GetValueNames()
    {
        return _values.Keys.ToList();
    }
    
    public void AddMacro(string name, Macro macro)
    {
        _macros[name] = macro;
    }
    
    public void AddPrimitive(string name, Primitive primitive)
    {
        _primitives[name] = primitive;
    }

    public void AddFunction(string name, Function function)
    {
        _functions[name] = function;
    }

    public void AddValue(string name, SymbolicExpression expr)
    {
        _values[name] = expr;
    }
    
    public bool TryGetMacro(string name, out Macro value)
    {
        if (_macros.TryGetValue(name, out var val))
        {
            value = val;
            return true;
        }
        
        if (Parent != null)
        {
            var p = Parent.TryGetMacro(name, out val);
            value = val;
            return p;
        }

        value = default;
        return false;
    }
    
    public bool TryGetPrimitive(string name, out Primitive value)
    {
        if (_primitives.TryGetValue(name, out var val))
        {
            value = val;
            return true;
        }
        
        if (Parent != null)
        {
            var p = Parent.TryGetPrimitive(name, out val);
            value = val;
            return p;
        }

        value = default;
        return false;
    }

    public bool TryGetFunction(string name, out Function value)
    {
        if (_functions.TryGetValue(name, out var val))
        {
            value = val;
            return true;
        }

        if (Parent != null)
        {
            var p = Parent.TryGetFunction(name, out val);
            value = val;
            return p;
        }

        value = default;
        return false;
    }

    public bool TryGetValue(string name, out SymbolicExpression value)
    {
        if (_values.TryGetValue(name, out var val))
        {
            value = val;
            return true;
        }

        if (Parent != null)
        {
            var p = Parent.TryGetValue(name, out val);
            value = val;
            return p;
        }

        value = default;
        return false;
    }
}
