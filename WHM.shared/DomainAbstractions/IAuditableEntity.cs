namespace appointmenting.DomainAbstractions;

public interface IAuditableEntity : IEntity
{
    string CreatedBy { get; }

    DateTime CreatedOn { get; }

    string? ModifiedBy { get; }

    DateTime? ModifiedOn { get; }
}


