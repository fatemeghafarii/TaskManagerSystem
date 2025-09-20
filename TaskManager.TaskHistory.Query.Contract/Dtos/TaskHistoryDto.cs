namespace TaskManager.TaskHistory.Query.Contract.Dtos;
public record TaskHistoryDto
{
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public string? OldStatus { get; set; }
    public string? NewStatus { get; set; }
    public Guid? ChangedBy { get; set; }
    public DateTime ChangedAt { get; set; }
}
