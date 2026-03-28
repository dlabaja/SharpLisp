namespace SharpLisp.Exceptions;

public class FunctionArgNotSymbolException(string funcName) : Exception($"Invalid arg type for {funcName}, needs to be a symbol");
