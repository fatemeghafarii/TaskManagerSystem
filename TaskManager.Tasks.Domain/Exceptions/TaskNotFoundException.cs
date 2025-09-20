namespace TaskManager.Tasks.Domain.Exceptions;
public class TaskNotFoundException(string? message) : DomainException(message)
{
}
