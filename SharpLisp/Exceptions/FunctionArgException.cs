namespace SharpLisp.Exceptions;

public class FunctionArgException(string message = "") : Exception($"Invalid args for function, {message ?? "no message provided"}");
