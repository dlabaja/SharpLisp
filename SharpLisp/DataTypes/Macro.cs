namespace SharpLisp.DataTypes;

public class Macro
{
    public Function Expander { get; }

    public Macro(Function expander)
    {
        Expander = expander;
    }

    public override string ToString()
    {
        return "#MACRO#";
    }
}
