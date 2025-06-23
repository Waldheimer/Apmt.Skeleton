namespace appointmenting.Exceptions;

public class RepositoryNotFoundException : Exception
{
    public RepositoryNotFoundException(string? message) : base(message) { }
}
