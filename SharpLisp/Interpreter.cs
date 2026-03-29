using SharpLisp.DataTypes;
using SharpLisp.Parsers;

namespace SharpLisp;

public static class Interpreter
{
    public static void Eval(string expr, out SymbolicExpression? result)
    {
        result = null;
        try
        {
            result = SharpLisp.Eval.Eval.Evaluate(
                Parser.Parse(
                    PreParser.PreParse(expr)));
            Console.WriteLine(result.ToString());
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.Message);
            Console.ResetColor();
        }
    }
}
