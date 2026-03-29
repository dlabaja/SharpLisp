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
        Add(PrimitiveNames.Sum, PrimitiveFunctions.SumPrimitive, env);
        Add(PrimitiveNames.Subtract, PrimitiveFunctions.SubtractPrimitive, env);
        Add(PrimitiveNames.Multiply, PrimitiveFunctions.MultiplyPrimitive, env);
        Add(PrimitiveNames.Divide, PrimitiveFunctions.DividePrimitive, env);
        Add(PrimitiveNames.Eql, PrimitiveFunctions.EqlPrimitive, env);
        Add(PrimitiveNames.Gt, PrimitiveFunctions.GtPrimitive, env);
        Add(PrimitiveNames.Sqrt, PrimitiveFunctions.SqrtPrimitive, env);
        Add(PrimitiveNames.Print, PrimitiveFunctions.PrintPrimitive, env);
        Add(PrimitiveNames.Cons, PrimitiveFunctions.ConsPrimitive, env);
        Add(PrimitiveNames.Car, PrimitiveFunctions.CarPrimitive, env);
        Add(PrimitiveNames.Cdr, PrimitiveFunctions.CdrPrimitive, env);
        Add(PrimitiveNames.List, PrimitiveFunctions.ListPrimitive, env);
        Add(PrimitiveNames.Atomp, PrimitiveFunctions.AtompPrimitive, env);
        Add(PrimitiveNames.Consp, PrimitiveFunctions.ConspPrimitive, env);
        Add(PrimitiveNames.Numberp, PrimitiveFunctions.NumberpPrimitive, env);
        Add(PrimitiveNames.Symbolp, PrimitiveFunctions.SymbolpPrimitive, env);
        Add(PrimitiveNames.Stringp, PrimitiveFunctions.StringpPrimitive, env);
        Add(PrimitiveNames.Functionp, PrimitiveFunctions.FunctionpPrimitive, env);
        Add(PrimitiveNames.Symbol, PrimitiveFunctions.SymbolPrimitive, env);
        env.AddValue("PI", SymbolicExpressionFactory.Float(double.Pi));
        env.AddValue("E", SymbolicExpressionFactory.Float(double.E));
        env.AddValue("NIL", SymbolicExpressionFactory.Nil);
        env.AddValue("T", SymbolicExpressionFactory.T);
        return env;
    }

    private static void Add(string name, Func<List<SymbolicExpression>, SymbolicExpression> func, Environment env)
    {
        env.AddPrimitive(name, new Primitive(name, func));
    }
}
