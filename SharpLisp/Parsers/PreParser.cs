using System.Text;

namespace SharpLisp.Parsers;

public static class PreParser
{
    private static void BracketsToNil(StringBuilder expr)
    {
        expr.Replace("()", "NIL");
    }
    
    public static string PreParse(string expr)
    {
        var builder = new StringBuilder(expr);
        BracketsToNil(builder);
        return builder.ToString();
    }
}
