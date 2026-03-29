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
        env.AddPrimitive(PrimitiveNames.Sum, new Primitive(PrimitiveFunctions.SumPrimitive));
        env.AddPrimitive(PrimitiveNames.Subtract, new Primitive(PrimitiveFunctions.SubtractPrimitive));
        env.AddPrimitive(PrimitiveNames.Multiply, new Primitive(PrimitiveFunctions.MultiplyPrimitive));
        env.AddPrimitive(PrimitiveNames.Divide, new Primitive(PrimitiveFunctions.DividePrimitive));
        env.AddPrimitive(PrimitiveNames.Eql, new Primitive(PrimitiveFunctions.EqlPrimitive));
        env.AddPrimitive(PrimitiveNames.Gt, new Primitive(PrimitiveFunctions.GtPrimitive));
        env.AddPrimitive(PrimitiveNames.Sqrt, new Primitive(PrimitiveFunctions.SqrtPrimitive));
        env.AddPrimitive(PrimitiveNames.Print, new Primitive(PrimitiveFunctions.PrintPrimitive));
        env.AddPrimitive(PrimitiveNames.Cons, new Primitive(PrimitiveFunctions.ConsPrimitive));
        env.AddPrimitive(PrimitiveNames.Car, new Primitive(PrimitiveFunctions.CarPrimitive));
        env.AddPrimitive(PrimitiveNames.Cdr, new Primitive(PrimitiveFunctions.CdrPrimitive));
        env.AddPrimitive(PrimitiveNames.List, new Primitive(PrimitiveFunctions.ListPrimitive));
        env.AddPrimitive(PrimitiveNames.Atomp, new Primitive(PrimitiveFunctions.AtompPrimitive));
        env.AddPrimitive(PrimitiveNames.Consp, new Primitive(PrimitiveFunctions.ConspPrimitive));
        env.AddPrimitive(PrimitiveNames.Numberp, new Primitive(PrimitiveFunctions.NumberpPrimitive));
        env.AddPrimitive(PrimitiveNames.Symbolp, new Primitive(PrimitiveFunctions.SymbolpPrimitive));
        env.AddPrimitive(PrimitiveNames.Stringp, new Primitive(PrimitiveFunctions.StringpPrimitive));
        env.AddPrimitive(PrimitiveNames.Functionp, new Primitive(PrimitiveFunctions.FunctionpPrimitive));
        env.AddPrimitive(PrimitiveNames.Symbol, new Primitive(PrimitiveFunctions.SymbolPrimitive));
        env.AddValue("PI", SymbolicExpressionFactory.Float(double.Pi));
        env.AddValue("E", SymbolicExpressionFactory.Float(double.E));
        env.AddValue("NIL", SymbolicExpressionFactory.Nil);
        env.AddValue("T", SymbolicExpressionFactory.T);
        return env;
    }
}
