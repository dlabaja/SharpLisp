namespace SharpLisp.Exceptions;

public class FunctionArgCountException(int required, int has) : Exception($"Invalid args for function, required {required}, got {has}");
