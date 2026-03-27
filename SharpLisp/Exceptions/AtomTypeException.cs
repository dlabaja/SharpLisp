namespace SharpLisp.Exceptions;

public class AtomTypeException(Type actualType, Type type) : Exception($"Atom is typeof {actualType}, tried to parse it to {type} instead");
