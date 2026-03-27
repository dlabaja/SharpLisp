namespace SharpLisp.Exceptions;

public class FunctionArgNotAtomException(string funcName) : Exception($"Invalid arg type for {funcName}, needs to be an atom");
