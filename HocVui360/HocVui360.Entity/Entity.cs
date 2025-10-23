namespace HocVui360.Entity;

internal interface IEntity<out TId>
    where TId : IEquatable<TId>, IComparable<TId>
{
    TId Id { get; }
}

public abstract class Entity<TId> : IEntity<TId>, IAuditable, ISoftDelete
    where TId : IEquatable<TId>, IComparable<TId>
{
    public TId Id { get; protected set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public byte[] RowVersion { get; set; }
    public bool IsDeleted { get; set; }
}

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}

public interface IAuditable
{
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
    byte[] RowVersion { get; set; }
}