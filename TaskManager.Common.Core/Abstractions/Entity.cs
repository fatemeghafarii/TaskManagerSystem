namespace TaskManager.Common.Core.Abstractions
{
    public abstract class Entity<TKey>
    {
        public TKey? Id { get; protected set; }
    }

    public abstract class AuditableEntity<TKey> : Entity<TKey>
    {
        public DateTime CreatedAt { get; protected set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; protected set; }
    }
}
