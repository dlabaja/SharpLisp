namespace SharpLisp.Exceptions;

public class FunctionArgCountException(string funcName, int required, int has) : Exception($"Invalid arg count for {funcName}, required {required}, got {has}");
