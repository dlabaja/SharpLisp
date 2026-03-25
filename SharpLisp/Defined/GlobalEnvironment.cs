using SharpLisp.DataTypes;
using Environment = SharpLisp.DataTypes.Environment;

namespace SharpLisp.Defined;

public static class GlobalEnvironment
{
    public static Environment Environment { get; } = GetGlobalEnvironment();

    private static Environment GetGlobalEnvironment()
    {
        var env = new Environment(null);
        env.AddPrimitive("+", new Primitive(2, PrimitiveFunctions.SumPrimitive));
        return env;
    }
}
