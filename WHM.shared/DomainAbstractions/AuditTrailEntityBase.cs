namespace appointmenting.DomainAbstractions;

public abstract class AuditTrailEntityBase : IAuditTrailEntity
{
    public Guid Id { get; protected set; } = default!;
    public Guid ReferenceId { get; set; }
    public string Action { get; set; } = default!;
    public string OldValues { get; set; } = default!;
    public string NewValues { get; set; } = default!;
    public DateTime ChangedOn { get; set; }

}
public abstract class AuditTrailEntityBase<TEntity> : IAuditTrailEntity<TEntity> where TEntity : class
{
    public Guid Id { get; protected set; } = default!;
    public TEntity OldValues { get; set; } = default!;
    public TEntity NewValues { get; set; } = default!;
    public Guid ReferenceId { get; set; }
    public string Action { get; set; } = default!;
    public DateTime ChangedOn { get; set; }
}


