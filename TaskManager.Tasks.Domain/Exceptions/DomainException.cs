namespace TaskManager.Tasks.Domain.Exceptions;

public abstract class DomainException(string? message) : Exception(message)
{
}
