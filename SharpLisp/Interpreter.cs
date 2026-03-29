using SharpLisp.Parsers;

namespace SharpLisp;

public static class Interpreter
{
    public static void Eval(string expr)
    {
        try
        {
            var exp = SharpLisp.Eval.Eval.Evaluate(
                Parser.Parse(
                    PreParser.PreParse(expr)));
            Console.WriteLine(exp.ToString());
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.Message);
            Console.ResetColor();
        }
    }
}
