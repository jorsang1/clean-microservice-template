namespace CleanCompanyName.DDDMicroservice.Domain.Common;

public abstract record AuditableEntity
(
    DateTimeOffset CreatedOn,
    Guid CreatedBy,
    DateTimeOffset LastModifiedOn,
    Guid LastModifiedBy
)
{
    public DateTimeOffset LastModifiedOn { get; private set; } = LastModifiedOn;
    public Guid LastModifiedBy { get; private set; } = LastModifiedBy;

    protected void ModifiedBy(Guid modifiedBy)
    {
        LastModifiedOn = CommonDateTime.Now;
        LastModifiedBy = modifiedBy;
    }
}