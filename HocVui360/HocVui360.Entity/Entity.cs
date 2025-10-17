namespace HocVui360.Entity;

public abstract class Entity<TId> where TId : IEquatable<TId>
{
    public TId Id { get; set; }
}
