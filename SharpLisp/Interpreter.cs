using SharpLisp.Parsers;

namespace SharpLisp;

public static class Interpreter
{
    public static void Eval(string expr)
    {
        Console.WriteLine(SharpLisp.Eval.Eval.Evaluate(
            Parser.Parse(
                PreParser.PreParse(expr))));
    }
}
