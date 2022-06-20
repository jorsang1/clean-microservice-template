namespace CleanCompanyName.DDDMicroservice.Domain.Common;

public abstract record AuditableEntity
{
    public DateTimeOffset CreatedOn { get; }
    public Guid CreatedBy { get; }
    public DateTimeOffset? LastModifiedOn { get; private set; }
    public Guid? LastModifiedBy { get; private set; }

    protected AuditableEntity(Guid createdBy)
    {
        CreatedOn = CommonDateTime.Now;
        CreatedBy = createdBy;
        LastModifiedOn = CommonDateTime.Now;
        LastModifiedBy = createdBy;
    }

    public void ModifiedBy(Guid lastModifiedBy)
    {
        LastModifiedOn = CommonDateTime.Now;
        LastModifiedBy = lastModifiedBy;
    }
}