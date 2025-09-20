namespace TaskManager.Common.Persistence.EF;
public class OutboxEventItem
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string EventName { get; set; } = null!;
    public string EventType { get; set; } = null!;
    public string AggregateName { get; set; } = null!;
    public string AggregateType { get; set; } = null!;
    public DateTime? PublishedOn { get; set; }
    public bool IsPublished { get; set; }
    public string? Payload { get; set; }
}
