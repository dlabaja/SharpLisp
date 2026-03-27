namespace SharpLisp.Exceptions;

public class ParseException(string expr) : Exception($"Cannot parse the sexpression {expr}");
