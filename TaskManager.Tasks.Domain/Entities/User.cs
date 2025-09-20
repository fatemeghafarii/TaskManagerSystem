using TaskManager.Common.Core.Abstractions;

namespace TaskManager.Tasks.Domain.Entities;
public class User : Entity<Guid>
{
    public string? UserName { get; set; }
    public ICollection<Task> Tasks { get; set; } = new List<Task>();
}
