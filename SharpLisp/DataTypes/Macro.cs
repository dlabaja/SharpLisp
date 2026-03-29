namespace SharpLisp.DataTypes;

public class Macro
{
    public string Name { get; }
    public Function Expander { get; }

    public Macro(string name, Function expander)
    {
        Name = name;
        Expander = expander;
    }

    public override string ToString()
    {
        return $"#MACRO-{Name}#";
    }
}
