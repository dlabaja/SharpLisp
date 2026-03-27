namespace SharpLisp.Exceptions;

public class FunctionNotFoundException(string name) : Exception($"Function {name} wasn't found in environment");
