using System.Text;

namespace SharpLisp.Parsers;

public static class PreParser
{
    public static string ExpressionToUpper(char[] expr)
    {
        char prev = '\0';
        var canProcess = true;
        for (int i = 0; i < expr.Length; i++)
        {
            if (expr[i] == '"' && prev != '\\')
            {
                canProcess = !canProcess;
            }

            if (!canProcess)
            {
                continue;
            }

            expr[i] = char.ToUpper(expr[i]);
        }

        return new string(expr);
    }
    
    public static string PreParse(string expr)
    {
        var new_exp = ExpressionToUpper(expr.Trim().ToCharArray());
        var builder = new StringBuilder(new_exp);
        builder.Replace("()", "NIL");
        builder.Replace('\n', ' ');
        builder.Replace('\t', ' ');
        return builder.ToString();
    }
}
