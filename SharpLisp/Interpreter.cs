using SharpLisp.Parsers;

namespace SharpLisp;

public static class Interpreter
{
    public static void Eval(string expr)
    {
        var exp = SharpLisp.Eval.Eval.Evaluate(
            Parser.Parse(
                PreParser.PreParse(expr)));
        Console.WriteLine(exp.Cons.ListString());
    }
}
