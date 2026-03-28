namespace SharpLisp.DataTypes;

public class Symbol
{
    public string Name { get; }

    public Symbol(string name)
    {
        Name = name;
    }
    
    public override string ToString()
    {
        return Name.ToUpper();
    }
}
