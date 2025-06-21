namespace appointmenting.DomainAbstractions;

public abstract class AuditableEntityBase : IAuditableEntity
{
    public Guid Id { get; protected set; } = default!;
    public string CreatedBy { get; protected set; } = string.Empty;

    public DateTime CreatedOn { get; protected set; } = DateTime.Now!;

    public string? ModifiedBy { get; protected set; } = null!;

    public DateTime? ModifiedOn { get; protected set; } = null!;

}


