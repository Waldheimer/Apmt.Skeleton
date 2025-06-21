# Domain Abstractions

## Interfaces
- [x] IEntity
- [x] IAuditableEntity  
    Enables Auditing of Creator, CreationTime, Modifier and ModificationTime
- [x] IAuditTrailEntity  
    Enables Auditing of performed Action and the TimeStamp along with referenceID ( depending on data strategy ) to the old Data, the Old and the new Values 

## Abstract Classes
- [x] EntityBase
- [x] AuditableEntityBase
- [x] AuditTrailEntityBase