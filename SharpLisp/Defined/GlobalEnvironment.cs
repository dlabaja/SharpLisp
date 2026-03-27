using SharpLisp.DataTypes;
using SharpLisp.Factories;
using Environment = SharpLisp.DataTypes.Environment;

namespace SharpLisp.Defined;

public static class GlobalEnvironment
{
    public static Environment Environment { get; } = GetGlobalEnvironment();

    private static Environment GetGlobalEnvironment()
    {
        var env = new Environment(null);
        env.AddPrimitive("+", new Primitive(PrimitiveFunctions.SumPrimitive));
        env.AddPrimitive("-", new Primitive(PrimitiveFunctions.SubtractPrimitive));
        env.AddPrimitive("*", new Primitive(PrimitiveFunctions.MultiplyPrimitive));
        env.AddPrimitive("/", new Primitive(PrimitiveFunctions.DividePrimitive));
        env.AddPrimitive("sqrt", new Primitive(PrimitiveFunctions.SqrtPrimitive));
        env.AddValue("pi", SymbolicExpressionFactory.Float(double.Pi));
        env.AddValue("e", SymbolicExpressionFactory.Float(double.E));
        return env;
    }
}
