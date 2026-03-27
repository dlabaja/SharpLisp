using SharpLisp.DataTypes;
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
            PrintOutput(exp);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static void PrintOutput(SymbolicExpression expr)
    {
        if (expr.IsAtom())
        {
            Console.WriteLine(expr.Atom);
            return;
        }
        Console.WriteLine(expr.Cons!.ListString());
    }
}
