namespace SharpLisp.Exceptions;

public class FunctionArgNotFunctionException(string funcName) : Exception($"Invalid arg type for {funcName}, needs to be a function");
