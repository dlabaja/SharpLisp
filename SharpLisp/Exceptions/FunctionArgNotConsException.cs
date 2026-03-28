namespace SharpLisp.Exceptions;

public class FunctionArgNotConsException(string funcName) : Exception($"Invalid arg type for {funcName}, needs to be a cons");
