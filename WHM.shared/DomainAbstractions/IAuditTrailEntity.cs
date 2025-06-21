namespace appointmenting.DomainAbstractions;

public interface IAuditTrailEntity : IEntity
{

    Guid ReferenceId { get; set; }

    string Action { get; set; }

    string OldValues { get; set; }

    string NewValues { get; set; }

    DateTime ChangedOn { get; set; }
}
public interface IAuditTrailEntity<TEntity> : IEntity
    where TEntity : class
{

    Guid ReferenceId { get; set; }

    string Action { get; set; }
    TEntity OldValues { get; set; }

    TEntity NewValues { get; set; }
    DateTime ChangedOn { get; set; }
}

