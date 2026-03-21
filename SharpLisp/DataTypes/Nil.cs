namespace SharpLisp.DataTypes;

public class Nil
{
    public static Atom NIL { get; } = new Atom(new Nil(), typeof(Nil));

    public override string ToString()
    {
        return "NIL";
    }
}
