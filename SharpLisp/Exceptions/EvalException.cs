using SharpLisp.DataTypes;

namespace SharpLisp.Exceptions;

public class EvalException : Exception
{
    public EvalException(SymbolicExpression expr) : base($"Cannot evaluate expression {expr}")
    {
    }
    
    public EvalException() : base("Cannot evaluate expression")
    {
    }
}
