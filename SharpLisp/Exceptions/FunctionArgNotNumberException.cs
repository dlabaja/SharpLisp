namespace SharpLisp.Exceptions;

public class FunctionArgNotNumberException(string funcName) : Exception($"Invalid arg type for {funcName}, needs to be a number");
