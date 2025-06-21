namespace appointmenting.DomainAbstractions;

public abstract class EntityBase : IEntity
{
    public Guid Id { get; protected set; } = default!;
}


