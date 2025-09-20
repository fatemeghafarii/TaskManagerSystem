namespace TaskManager.Tasks.Query.Contract.Dtos;
public record TaskDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public Guid UserId { get; set; }
}
