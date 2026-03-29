using SharpLisp.DataTypes;
using SharpLisp.Parsers;

namespace SharpLisp;

public static class Interpreter
{
    public static SymbolicExpression Eval(string expr)
    {
        return SharpLisp.Eval.Eval.Evaluate(
            Parser.Parse(
                PreParser.PreParse(expr)));
    }
}
