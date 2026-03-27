using SharpLisp.DataTypes;

namespace SharpLisp.Exceptions;

public class EvalException(SymbolicExpression expr) : Exception($"Cannot evaluate expression {expr}");
