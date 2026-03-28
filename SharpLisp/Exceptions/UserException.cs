namespace SharpLisp.Exceptions;

public class UserException(string message) : Exception($"Exception invoked by user: {message}");
