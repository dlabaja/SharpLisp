namespace SharpLisp.Exceptions;

public class ValueNotFoundException(string name) : Exception($"Cannot find value {name}");
